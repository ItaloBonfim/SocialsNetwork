using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions
{
    public class ReactionPost
    {
        public static string Template => "api/Reaction/new/{publicationId}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody] ReactionRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            if (user == null) return Results.BadRequest();
            var pub = await context.Publication.FindAsync(request.PublicationId);
            var rc = await context.TypeReactions.FindAsync(request.reaction);

            if (user == null || pub == null)
                return Results.NotFound();

            var data = new Reaction(pub, user, rc);
            if (!data.IsValid)
                return Results.BadRequest();


            
            return Results.Ok(data.Id);
        }
    }
}
