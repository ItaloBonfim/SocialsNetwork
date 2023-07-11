using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.CommentsReaction
{
    public class CommentReactionPost
    {
        public static string Template => "api/reaction/comments/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action(HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            var user = await context.ApplicationUsers.FindAsync(LoggedUser); // usuario
            if (user == null)
                return Results.NotFound("usuario não identificado");
            
        


            return Results.Ok();
        }
    }
}
