using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Comments
{
    public class CommentGet
    {
        public static string Template => "api/comments/{publicationId}/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute]string publicationId, HttpContext http, FindPublicationComments Query, int page = 1, int rows = 24)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var data = Query.Execute(publicationId, page, rows);
            if(data == null)
                return Results.NotFound();

            return Results.Ok(Query.Execute(publicationId, page, rows));
        }
    }
}
