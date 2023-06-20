using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Subcomment
{
    public class SubcommentDelete
    {
        public static string Template => "api/sub/comments/delete/{sCommenteId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action([FromRoute] Guid sCommenteId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
          
           var data = context.SubComments.FirstOrDefault(
               x => x.Id == sCommenteId && x.UserId == LoggedUser);

            if (data == null) return Results.NotFound("Comentario não localizado");

            context.SubComments.Remove(data);
            await context.SaveChangesAsync();
            
            return Results.Ok();
        }
    }
}
