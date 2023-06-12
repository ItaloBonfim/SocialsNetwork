using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions
{
    public class ReactionGet
    {
        public static string Template => "api/Reaction/{publicationId}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] string publicationId, int page, int rows, HttpContext http, FindPublicationReaction Query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var data = Query.Execute(publicationId, page, rows);
            if (data == null)
                return Results.NoContent();


            return Results.Ok(data);
        }
    }
}
