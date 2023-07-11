using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Comments
{
    public class CommentDelete
    {
        public static string Template => "api/comments/delete/{pCommendId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action([FromRoute] Guid pCommendId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            #region CONSULTA DE DADOS PARA VALIDAÇÃO E EXCLUSÃO POSTERIOR
            var pc = await (from P in context.Publication
                            join C in context.Comments on P.Id equals C.Publication.Id
                            where
                            C.Id == pCommendId

                            select new
                            {
                                IdPublication = C.Publication,
                                userComment = C.UserId,
                                user = P.User.Id

                            }).FirstOrDefaultAsync();
            
            if(pc == null) return Results.NotFound();
            #endregion

            #region EXCLUSÃO DE DADOS POR MEIO DE VERIFICAÇÃO DO USUARIO LOGADO
            // NÃO COMENTARISTA MAS AUTOR
            if (!pc.userComment.Equals(LoggedUser) && pc.user.Equals(LoggedUser))
            {
                var data = context.Comments.Find(pCommendId);
                if(data == null) return Results.NotFound();

                context.Comments.Remove(data);
                //await context.SaveChangesAsync();
                return Results.Ok();
            }
            // COMENTARISTA MAS NÃO AUTOR
            if (pc.userComment.Equals(LoggedUser) && !pc.user.Equals(LoggedUser))
            {
                var data = context.Comments.Find(pCommendId);
                
                if (data == null) return Results.NotFound();
                context.Comments.Remove(data);

                //await context.SaveChangesAsync();
                return Results.Ok();
            }
            // AUTOR E COMENTARISTA
            if(pc.userComment.Equals(LoggedUser) && pc.user.Equals(LoggedUser))
            {
                var data = context.Comments.Find(pCommendId);

                if (data == null) return Results.NotFound();
                context.Comments.Remove(data);

                //await context.SaveChangesAsync();
                return Results.Ok();
            }
            #endregion

            return Results.Forbid();
        }
    }
}
