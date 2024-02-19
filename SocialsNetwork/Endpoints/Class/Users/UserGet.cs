using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Users
{
    public class UserGet
    {
        public static string Template => "api/user/{user:Guid?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;


        public static IResult Action(HttpContext http, FindAllUsersWithClaims query, string? user)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            // realizar validações      

            if (!string.IsNullOrEmpty(user)) {
                
                return Results.Ok(query.FindUserById(user));
            }
            return Results.Ok(query.FindUserById(LoggedUser));
        }
    }
}
