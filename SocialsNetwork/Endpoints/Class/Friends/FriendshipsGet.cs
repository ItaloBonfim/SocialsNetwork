using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Friends
{
    public class FriendshipsGet
    {
        public static string Template => "/api/friends/{user:Guid?}/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(GetAllFriends Query, HttpContext http, string? user, int page = 1, int rows = 24 )
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (user != null) return Results.Ok(Query.Execute(user, page, rows));
            //realizar validações 
            return Results.Ok(Query.Execute(LoggedUser, page, rows));
        }

    }
}
