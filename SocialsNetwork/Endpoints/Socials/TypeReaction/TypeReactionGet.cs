using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.TypeReaction
{
    public class TypeReactionGet
    {
        public static string Template => "api/avaliable/reactions";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        public static IResult Action(HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var data = (from aReactions in context.TypeReactions
                        orderby aReactions.Description
                        select aReactions).ToList();
            if (data == null)
                return Results.NoContent();

            return Results.Ok(data);
        }
    }
}
