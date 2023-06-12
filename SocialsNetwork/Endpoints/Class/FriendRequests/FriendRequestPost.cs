using SocialsNetwork.DTO.Class;
using SocialsNetwork.Endpoints.DicionaryErrors;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Class;
using SocialsNetwork.Models.Class.Enums;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.FriendRequests
{
    public class FriendRequestPost
    {
        public static string Template => "api/friendRequests";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;
        public static async Task<IResult> Action(FriendInviteManager request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if(LoggedUser == null) return Results.Forbid();
           
            if (request.status.Equals(RequestStatus.accepted))
            {
                // inserir novo registro na tabela friendships
                return await AddNewFriendship(LoggedUser, request, context);
            } else if (request.status.Equals(RequestStatus.denied)
                        || request.status.Equals(RequestStatus.canceled))
            {
                // apagar a solicitação de amizade de FriendRequest
               return await RemoveFriendshipRequest(request.Id, context);   
            }

            return Results.Ok();
        }

        private static async Task<IResult> AddNewFriendship(string LoggedUser, FriendInviteManager request, AppDbContext context)
        {
            /* Verifica e retorna os dados do usuario */
            var asked = context.ApplicationUsers.FindAsync(LoggedUser).Result;
            var askFriendship = context.ApplicationUsers.FindAsync(request.AskFriendship).Result;
            if (asked == null || askFriendship == null) 
                return Results.NotFound("Usuario não identificado"); 
            
            /* Verifica o registro e retorna a solicitação */
            var register = context.FriendRequests.FindAsync(request.Id).Result;
            if (register == null) 
                return Results.NotFound("Registro da solicitação não encontrado");

            /* Verifica se o usuario que recebeu a solicitação é o mesmo que o logado */
            if (!IsRequested(asked.Id, request.Asked))
                return Results.BadRequest();

            /* Cria a instancia de uma nova amizade e realiza a validação usando os dados de usuario
             * obtidos anteriormente 
             */
            var data = new Friendships(askFriendship, asked);
            if (!data.IsValid)
                return Results.ValidationProblem(data.Notifications.convertToDetails());

            await context.Friendships.AddAsync(data);
            await RemoveFriendshipRequest(request.Id, context);
            await context.SaveChangesAsync();

            return Results.Created($"/Friend/{data.Id}", data.Id);
        }

        private static async Task<IResult> RemoveFriendshipRequest(Guid IdRequest, AppDbContext context)
        {
            var remove = context.FriendRequests.FindAsync(IdRequest).Result;

            if (remove == null) 
                return Results.NotFound();

            context.FriendRequests.Remove(remove);
            await context.SaveChangesAsync();
            return Results.Ok();
        }
        private static bool IsRequested(string Requested, string requestId) 
        {
            return Requested == requestId;
        }

    }
}
