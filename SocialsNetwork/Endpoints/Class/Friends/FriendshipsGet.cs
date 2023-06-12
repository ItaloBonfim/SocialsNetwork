using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Friends
{
    public class FriendshipsGet
    {
        public static string Template => "/friends";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, int page, int rows, GetAllFriends query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            //realizar validações 
            return Results.Ok(query.Execute(LoggedUser, page, rows));
        }

    }
}
