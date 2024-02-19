using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Follows
{
    public class SearchFilterFollows
    {
        public static string Template => "api/follow/see/search/{filter:alpha?}/{userId:Guid?}/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, GetAllFollowsUsers Query,
                                    string? userId, string? filter, int page = 1, int rows = 48)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if(userId != null)
            {
                return Results.Ok(Query.FilterFollows(userId, filter, page, rows));
            }

            return Results.Ok(Query.FilterFollows(LoggedUser, filter, page, rows));
        }
    }
}
