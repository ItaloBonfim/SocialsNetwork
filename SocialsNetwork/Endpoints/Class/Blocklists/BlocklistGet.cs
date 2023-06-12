using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Blocklists
{
    public class BlocklistGet
    {
        public static string Template => "api/blocklist/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, int? page, int? rows, FindBlockListUsers Query)
        {   
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (LoggedUser == null) return Results.Forbid();
    
            
            return Results.Ok(Query.Execute(LoggedUser, page, rows ));
        }
    }
}
