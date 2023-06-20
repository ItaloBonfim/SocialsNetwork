using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions
{
    public class ReactionDelete
    {
        public static string Template => "api/reaction/delete/{reactionId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action([FromRoute] Guid reactionId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var data = context.Reaction.FirstOrDefault(
                x => x.Id == reactionId && x.UserId == LoggedUser);

            if (data == null)
                return Results.NotFound("Reação não foi identificada");

            context.Reaction.Remove(data);
            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
