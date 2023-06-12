using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Comments
{
    public class CommentDelete
    {
        public static string Template => "api/comments/delete/{pCommendId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action([FromRoute] Guid pCommendId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (LoggedUser == null)
                return Results.Forbid();

            var data = context.Comments.FirstOrDefault(
                x => x.Id == pCommendId && x.User.Id == LoggedUser);
            if (data == null)
                return Results.NotFound("dados não localizados");

             context.Comments.Remove(data);
             await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
