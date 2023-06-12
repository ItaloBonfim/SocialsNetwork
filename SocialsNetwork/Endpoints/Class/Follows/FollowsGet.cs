using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Follows
{
    public class FollowsGet
    {
        public static string Template => "api/follow/see/following/{userId:Guid?}/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, GetAllFollowsUsers query, string? userId, int page = 1, int rows = 24)
        {
            if(http.User == null) return Results.Forbid();

            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if(userId != null)
            {
                return Results.Ok(query.ExecuteFollowing(userId, page, rows));
            }
                        
            return Results.Ok(query.ExecuteFollowing(LoggedUser,page, rows));
        }
    }
}
