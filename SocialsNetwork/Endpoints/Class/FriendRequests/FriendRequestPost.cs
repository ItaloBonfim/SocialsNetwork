using Azure.Core;
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
                // inserir novo registro na tabela friendships e apaga o registro da tabela FriendRequest
                return await AddNewFriendship(LoggedUser, request, context);
            } else if (request.status.Equals(RequestStatus.denied))
            {
                // apagar a solicitação de amizade de FriendRequest
                return await RemoveFriendshipRequest(LoggedUser, request, context);   
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
                return Results.Forbid();

            /* Cria a instancia de uma nova amizade e realiza a validação usando os dados de usuario
             * obtidos anteriormente 
             */
            var data = new Friendships(askFriendship, asked);
            if (!data.IsValid)
                return Results.ValidationProblem(data.Notifications.convertToDetails());

            await context.Friendships.AddAsync(data);
            await RemoveFriendshipRequest(LoggedUser, request, context);
            await context.SaveChangesAsync();
            return Results.Created($"/Friend/{data.Id}", data.Id);
        }

        private static async Task<IResult> RemoveFriendshipRequest(string LoggedUser, FriendInviteManager request, AppDbContext context)
        {
           
            var asked = context.ApplicationUsers.FindAsync(LoggedUser).Result; 
            var askFriendship = context.ApplicationUsers.FindAsync(request.AskFriendship).Result;
            
            if (asked == null || askFriendship == null)
                return Results.NotFound("Usuario não identificado");

            var remove = context.FriendRequests.FindAsync(request.Id).Result;
            if (remove == null) return Results.NotFound("Requisição não identificada");

            if (remove.Asked == null || remove.Asked.Id != asked.Id) return Results.Forbid();

            context.FriendRequests.Remove(remove);
            /* Verifica se o status da requição é negado para casos onde o usuario negou a solicitação de amizade e salva */
            if(request.status.Equals(RequestStatus.denied)) await context.SaveChangesAsync();

            return Results.Ok();
        }
        private static bool IsRequested(string Requested, string requestId) 
        {
            return Requested == requestId;
        }

    }
}
