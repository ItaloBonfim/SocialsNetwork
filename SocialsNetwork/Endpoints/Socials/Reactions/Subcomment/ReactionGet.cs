using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Subcomment
{
    public class ReactionGet
    {
        public static string Template => "api/reaction/answer/{asnwerID:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action([FromRoute] Guid asnwerID, HttpContext http, AppDbContext context)
        {
            var Lista = await (from RASW in context.CommentReactions
                               where
                               RASW.SubComment.Id == asnwerID
                               select new
                               {
                                   RASW,
                                   RASW.User

                               }).Take(24).OrderByDescending(X => X.RASW.CreatedOn)
                               .ToListAsync();
                            
            if (!Lista.Any())
                return Results.NotFound("Não foi encontrado quaisquer reações sobre essa resposta");

            return Results.Ok(Lista);
        }
    }
}
