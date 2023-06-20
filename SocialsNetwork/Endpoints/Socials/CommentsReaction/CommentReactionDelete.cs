using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.CommentsReaction
{
    public class CommentReactionDelete
    {
        public static string Template => "api/reaction/comments/delete/{commentReactId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action([FromRoute] Guid commentReactId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            var data = context.CommentReactions.FirstOrDefault
                (x => x.Id == commentReactId && x.UserId == LoggedUser);

            if (data == null)
                return Results.NotFound("Reação ao comentario não identificada");

            context.CommentReactions.Remove(data);
            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
