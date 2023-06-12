using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Streams.StChannel
{
    public class StreamChannelGet
    {
        public static string Template => "";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, FindChannels Query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            return Results.Ok();
        }
    }
}
