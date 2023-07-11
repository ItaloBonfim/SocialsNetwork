using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Subcomment
{
    public class ReactionPost
    {
        public static string Template => "api/Reaction/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody] ReactionPublicationRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            if (user == null) return Results.BadRequest();

            var data = new Reaction();
            if (!data.IsValid)
                return Results.BadRequest();



            return Results.Ok();
        }

    }
}
