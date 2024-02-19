using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Users
{
    public class UserGetAll
    {
        public static string Template => "api/user/All/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;


        public static IResult Action(HttpContext http, FindAllUsersWithClaims query, int page = 1, int rows = 24)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // realizar validações      
            return Results.Ok(query.Execute(LoggedUser, page, rows));
        }
    }
}
