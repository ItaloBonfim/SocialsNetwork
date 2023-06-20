using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Publications
{
    public class PublicationGet
    {
        public static string Template => "api/publications/{user:Guid?}/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, FindPublicationsWithClaims Query, string? user,int page = 1, int rows = 24)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var data = Query.PerfilPublications(LoggedUser, page, rows);
            if (data == null)
                //há outras regras a aplicar
                return Results.Forbid();

            return Results.Ok(data);
        }
    }
}
