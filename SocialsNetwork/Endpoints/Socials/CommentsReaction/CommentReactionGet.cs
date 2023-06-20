using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.CommentsReaction
{
    public class CommentReactionGet
    {
        public static string Template => "api/reaction/comments/{publicationId}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] string publicationId, int page, int rows, HttpContext http, FindReactionsComment Query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            var data = Query.Execute(publicationId, page, rows);
            if (data == null)
                return Results.NotFound();

            return Results.Ok(data);
        }
    }
}
