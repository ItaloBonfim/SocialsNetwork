using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.DTO.Class;

namespace SocialsNetwork.Endpoints.Class.FriendRequests
{
    public class FriendRequestDelete
    {
        public static string Template => "api/friendRequest/delete/{Id}";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;
        public static IResult Action([FromRoute] Guid Id,HttpContext http, AppDbContext context)
        {

            //var Requisition = context.FriendRequests.Where(arg => arg.Id == Id).FirstOrDefault();
            var response = context.FriendRequests.FindAsync(Id).Result;
            
            
            if (response == null) return Results.NotFound();

            context.FriendRequests.Remove(response);
            context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}

