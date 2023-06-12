using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.CommentsReaction
{
    public class CommentReactionPost
    {
        public static string Template => "api/reaction/comments/new";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action([FromBody] CommentReactionRequest request,HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();
            
            var user = await context.ApplicationUsers.FindAsync(LoggedUser); // usuario
            if (user == null)
                return Results.NotFound("usuario não identificado");
            
            var cmm = context.Comments.FirstOrDefault(
                x => x.Id == request.pCommentId); // comentario
            var scm = context.SubComments.FirstOrDefault(
                x => x.Id == request.sCommentId); // sub comentario

            if (cmm == null && scm == null)
                return Results.NotFound("comentarios não identificados");

            var reaction = context.TypeReactions.FirstOrDefault(
                x => x.Id == request.ReactType);
            if (reaction == null)
                return Results.BadRequest("Confira o input de dados");


            var data = new CommentReaction(cmm, scm, user, reaction);
            if (!data.IsValid)
                return Results.BadRequest("Erro durante a validação");


            return Results.Ok(data);
        }
    }
}
