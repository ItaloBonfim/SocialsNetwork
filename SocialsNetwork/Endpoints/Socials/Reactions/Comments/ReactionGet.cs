using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Comments
{
    public class ReactionGet
    {
        public static string Template => "api/reaction/comment/{commentId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action([FromRoute] Guid commentId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var Lista = await (from RCR in context.CommentReactions
                               where
                               RCR.Comment.Id == commentId
                               select new
                               {
                                   RCR,
                                   RCR.User
                                   
                               }).Take(24).OrderByDescending(X => X.RCR.CreatedOn)
                               .ToListAsync();
            if (!Lista.Any()) 
                return Results.NotFound("Não foi encotnrado quaisquer reações sobre esse comentario");
            
            return Results.Ok(Lista);
        }
    }
}
