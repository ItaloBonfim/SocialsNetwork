using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Publications
{
    public class PublicationGet
    {
        public static string Template => "api/publications";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(int page, int rows, HttpContext http, FindPublicationsWithClaims Query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var data = Query.Execute(LoggedUser, page, rows);
            if (data == null)
                //há outras regras a aplicar
                return Results.Forbid();

            return Results.Ok(data);
        }
    }
}
