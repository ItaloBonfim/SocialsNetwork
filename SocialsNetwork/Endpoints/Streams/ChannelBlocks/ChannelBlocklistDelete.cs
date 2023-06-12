using SocialsNetwork.Infra.Data;
using System.Security.Claims;
using static System.Net.WebRequestMethods;

namespace SocialsNetwork.Endpoints.Streams.ChannelBlocks
{

    public class ChannelBlocklistDelete
    {
        public static string Template => "api/channel/blocklist/new";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (LoggedUser == null) return Results.NotFound();


           return Results.Ok();
        }

      


        
    }
}
