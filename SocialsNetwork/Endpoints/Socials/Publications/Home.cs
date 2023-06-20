using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Publications
{
    public class Home
    {

        public static string Template => "api/home/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, FindPublicationsWithClaims Query, int page = 1, int rows = 24)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
           
            var data = Query.Execute(LoggedUser, page, rows);
            if (data == null)
                //há outras regras a aplicar
                return Results.NotFound("Não encontrado publicações"); 

            return Results.Ok(data);
        }
    }
}
