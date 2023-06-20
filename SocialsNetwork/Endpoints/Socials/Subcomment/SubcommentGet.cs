using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Subcomment
{
    public class SubcommentGet
    {
        public static string Template => "api/sub/comments/{pCommentId}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action([FromRoute] string pCommentId,int page, int rows,HttpContext http, FindSubComments Query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var data = Query.Execute(pCommentId, page, rows);
            if (data == null)
                return Results.NoContent();

            return Results.Ok(data);
        }
    }
}
