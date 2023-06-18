using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Comments
{
    public class CommentGet
    {
        public static string Template => "api/comments/{publicationId}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute]string publicationId, int page, int rows,HttpContext http, FindPublicationComments Query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var data = Query.Execute(publicationId, page, rows);
            if(data == null)
                return Results.NotFound();

            return Results.Ok();
        }
    }
}
