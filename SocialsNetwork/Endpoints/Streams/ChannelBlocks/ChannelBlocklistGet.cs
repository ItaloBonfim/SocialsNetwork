using Microsoft.AspNetCore.Identity;
using SocialsNetwork.Infra.Data.CustomQueries;

namespace SocialsNetwork.Endpoints.Streams.ChannelBlocks
{
    public class ChannelBlocklistGet
    {
        public static string Template => "api/channelBlocklist/{userID}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(string userID, HttpContext contex, FindChannelBlockList Query)
        {

            return Results.Ok();
        }
    }
}

