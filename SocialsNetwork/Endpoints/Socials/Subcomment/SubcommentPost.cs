using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Subcomment
{
    public class SubcommentPost
    {
        public static string Template => "api/sub/comments/new";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody] SubcommentRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            //var cmm = await context.Comments.FindAsync(request.pCommentId);
            var cmm = context.Comments.FirstOrDefault(
                x => x.Id == request.pCommentId);
            
            if (user == null || cmm == null)
                return Results.NotFound("Dados necessario não identificados");

            var data = new SubComment(cmm, user, request.CommentValue, request.ImageURL, request.MidiaURL);
            if (!data.IsValid)
                return Results.BadRequest("Falha na validação");

            await context.SubComments.AddAsync(data);
            await context.SaveChangesAsync();

            return Results.Ok(data);
        }
    }
}
