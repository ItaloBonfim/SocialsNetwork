using SocialsNetwork.DTO.Class.Enums;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.FriendRequests
{
    public class FriendRequestGet
    {
        public static string Template => "api/friendRequests/{status:int?}/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, GetAllFriendInvite Query, RequestStatus? status, int page = 1, int rows = 24)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.BadRequest("Usuario não autenticado");


            if (status.Equals(RequestStatus.awaiting))
            {
                return Results.Ok(Query.InvitationsSent(LoggedUser, page, rows));
            }

            //realizar validações
            return Results.Ok(Query.Execute(LoggedUser, page, rows));
        }
    }
}
