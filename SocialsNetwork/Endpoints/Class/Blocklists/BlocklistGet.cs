
using SocialsNetwork.Business.Class.Bloqueios;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Interfaces.Class.Business;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Blocklists
{
    public class BlocklistGet
    {
        public static string Template => "api/blocklist/{page:int?}/{rows:int?}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        public static IResult Action(HttpContext http, FindBlockListUsers Query, int page = 1, int rows = 24)
        {   
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            //List<UserResponseBlock> Lista = obj.ListarUsuariosBloqueados(LoggedUser, page, rows);

            //return Results.Ok(Query.Execute(LoggedUser, page, rows));
            IBlock obj = new ControleBloqueio();
            return Results.Ok(obj.ListarUsuariosBloqueados(Query,LoggedUser, page, rows));
        }
    }
}
