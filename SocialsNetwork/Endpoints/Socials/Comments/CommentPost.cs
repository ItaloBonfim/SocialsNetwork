using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Comments
{
    public class CommentPost
    {
        public static string Template => "api/comments/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody] CommentsRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            if (user == null) return Results.BadRequest();

            var pub = await context.Publication.FirstOrDefaultAsync(
                        x => x.Id == request.Publication);

            if (user == null || pub == null)
                return Results.NotFound("Informações necessarias não identificadas");

           var data =
                new Comment(pub, user, request.Comment, request.ImageURL, request.MidiaURL);
            if (!data.IsValid)
                return Results.BadRequest("Falha na validação");

            await context.Comments.AddAsync(data);
            await context.SaveChangesAsync();
            return Results.Ok(data.Id);
        }
    }
}
