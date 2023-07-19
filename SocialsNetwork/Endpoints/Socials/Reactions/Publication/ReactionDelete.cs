using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Publication
{
    public class ReactionDelete
    {
        public static string Template => "api/reaction/publication/delete/{reactionId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;
        public static async Task<IResult> Action([FromRoute] Guid reactionId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var data = await context.Reaction.FirstOrDefaultAsync(
                x => x.Id == reactionId && x.UserId == LoggedUser);

            if (data == null)
                return Results.NotFound("Reação não foi identificada");

            context.Reaction.Remove(data);
            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
