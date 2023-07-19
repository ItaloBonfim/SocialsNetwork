using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Comments
{
    public class ReactionDelete
    {
        public static string Template => "api/reaction/comment/delete/{reactionId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action([FromRoute] Guid reactionId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var CommentReaction = await (from CR in context.CommentReactions
                                     where CR.Id == reactionId && CR.UserId == LoggedUser

                                     select new { CR }).FirstOrDefaultAsync();

            if (CommentReaction == null) return Results.NotFound("Reação não encontrada.");

            context.Remove(CommentReaction);
            await context.SaveChangesAsync();
            return Results.Ok();
        }
    }
}
