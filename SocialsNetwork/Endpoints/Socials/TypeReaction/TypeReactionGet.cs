using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.TypeReaction
{
    public class TypeReactionGet
    {
        public static string Template => "api/avaliable/reactions";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        public static IResult Action(HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var lista = (from aReactions in context.TypeReactions
                         orderby aReactions.Description
                         select aReactions).OrderBy(aReactions => aReactions.Id).ToList();
            
            if (lista == null)
                return Results.NoContent();

            // OrderByDescending(X => X.RASW.CreatedOn)
            List<TypesReactions> Types = new List<TypesReactions>();

            foreach(var item in lista)
            {
                Types.Add(new TypesReactions(item.Id, item.Description));                    
            }

            return Results.Ok(Types);
        }
    }
}
