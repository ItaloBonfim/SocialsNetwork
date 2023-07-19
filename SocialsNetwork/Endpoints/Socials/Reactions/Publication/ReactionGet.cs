using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Publication
{
    public class ReactionGet
    {
        public static string Template => "api/reaction/publication/{publicationId:guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute] Guid publicationId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var Lista = await (from RPUB in context.Publication
                               where
                               RPUB.Id == publicationId
                               select new
                               {
                                   RPUB,
                                   RPUB.User

                               }).Take(24).OrderByDescending(X => X.RPUB.CreatedOn)
                               .ToListAsync();
                              
            if (!Lista.Any())
                return Results.NotFound("Não foi encotnrado quaisquer reações sobre essa publicação");

            return Results.Ok(Lista);
        }
    }
}
