using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.FriendRequests
{
    public class FriendRequestCancel
    {
        public static string Template => "api/friendRequests/{invitationId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;
        public static async Task<IResult> Action([FromRoute]Guid invitationId, HttpContext http, AppDbContext context)
        {

            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var askFriendship = context.ApplicationUsers.FindAsync(LoggedUser).Result;

            if(askFriendship == null) return Results.NotFound("Usuario não autorizado!");

            
            var remove = context.FriendRequests.FindAsync(invitationId).Result;
            if (remove == null) return Results.NotFound("Solicitação de amizade não identificada...");


            if (remove.AskFriendship == null ||
                    !remove.AskFriendship.Id.Equals(askFriendship.Id)) return Results.Forbid();
           
            context.FriendRequests.Remove(remove);
            await context.SaveChangesAsync();
            return Results.Ok();

        }
    }
}
