using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Users
{
    public class UserGet
    {
        public static string Template => "api/user";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(int? page,int? rows, HttpContext http, FindAllUsersWithClaims query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            // realizar validações      
            return Results.Ok(query.Execute(LoggedUser, page.Value, rows.Value));
        }
    }
}
