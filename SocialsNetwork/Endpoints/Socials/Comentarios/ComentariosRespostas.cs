using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Comentarios
{
    public class ComentariosRespostas
    {
        public static string Template => "api/comments/deleteX/{Type:int?}/{Id:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action([FromRoute] int? Type, Guid Id, HttpContext http, AppDbContext context)
        {
            
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
           
            var pcs = await (
                        from P in context.Publication
                        join C in context.Comments on P.Id equals C.Publication.Id
                        join S in context.SubComments on C.Id equals S.Comment.Id
                        where
                        C.Id == Id || S.Id == Id
                        select new
                        {
                            pAutor = P.UserId,
                            cAutor = C.UserId,
                            sAutor = S.UserId,

                            pId = P.Id,
                            cId = C.Id,
                            sId = S.Id

                        }).FirstOrDefaultAsync();

            if (pcs == null) return Results.NotFound();


            var dataContent = await (
            from P in context.Publication
            join C in context.Comments on P.Id equals C.Publication.Id
            join S in context.SubComments on C.Id equals S.Comment.Id
            where
            C.Id == Id || S.Id == Id
            select new
            {
                P, C, S

            }).FirstOrDefaultAsync();

            if (dataContent == null) return Results.NotFound();

            


            #region CENARIO DE EXCLUSÃO DE UMA RESPOSTA
            if (Type == 1) 
            {
                if (pcs.pAutor.Equals(LoggedUser) && pcs.sAutor.Equals(LoggedUser))
                {
                    var data = await context.SubComments.FindAsync(pcs.sId);
                    if (data == null) return Results.NotFound("Resposta não localizada #005");

                    context.SubComments.Remove(data);
                    //await context.SaveChangesAsync();
                    return Results.Ok();
                }

                if (pcs.pAutor.Equals(LoggedUser) && !pcs.sAutor.Equals(LoggedUser))
                {
                    var data = context.SubComments.Find(pcs.sId);
                    if (data == null) return Results.NotFound("Resposta não localizada #005");

                    context.SubComments.Remove(data);
                    //await context.SaveChangesAsync();
                    return Results.Ok();
                }

                if (pcs.cAutor.Equals(LoggedUser) && pcs.sAutor.Equals(LoggedUser))
                {
                    var data = context.SubComments.Find(pcs.sId);
                    if (data == null) return Results.NotFound("Resposta não localizada #005");

                    context.SubComments.Remove(data);
                    //await context.SaveChangesAsync();
                    return Results.Ok();
                }

                if (pcs.cAutor.Equals(LoggedUser) && !pcs.sAutor.Equals(LoggedUser))
                {
                    /*
                     * PARA EVITAR QUE O AUTOR DA PUBLICAÇÃO TENHA SUA RESPOSTA AO COMENTARIO 
                     * APAGADO PELO AUTOR DO COMENTARIO
                     **/
                    if (pcs.sAutor == pcs.pAutor) return Results.Forbid();

                    var data = context.SubComments.Find(pcs.sId);
                    if (data == null) return Results.NotFound("Resposta não localizada #005");

                    context.SubComments.Remove(data);
                    //await context.SaveChangesAsync();
                    return Results.Ok();
                }

                if (!pcs.cAutor.Equals(LoggedUser) && pcs.sAutor.Equals(LoggedUser))
                {
                    var data = context.Comments.Find(pcs.cId);
                    if (data == null) return Results.NotFound("Resposta não localizada #005");

                    context.Comments.Remove(data);
                    //await context.SaveChangesAsync();
                    return Results.Ok();
                }
            }
            #endregion

            #region CENARIO DE EXCLUSÃO DE UM COMENTARIO 
            
            if (!pcs.cAutor.Equals(LoggedUser) && pcs.pAutor.Equals(LoggedUser))
            {
                var data = context.Comments.Find(pcs.cId);
                if (data == null) return Results.NotFound("Resposta não localizada #004");

                context.Comments.Remove(data);
                //await context.SaveChangesAsync();
                return Results.Ok();
            }

            if (pcs.cAutor.Equals(LoggedUser) && !pcs.pAutor.Equals(LoggedUser))
            {
                var data = context.Comments.Find(pcs.cId);

                if (data == null) return Results.NotFound("Resposta não localizada #004");
                context.Comments.Remove(data);

                //await context.SaveChangesAsync();
                return Results.Ok();
            }

            if (pcs.pAutor.Equals(LoggedUser) && pcs.cAutor.Equals(LoggedUser))
            {
                var data = context.Comments.Find(pcs.cId);

                if (data == null) return Results.NotFound("Resposta não localizada #004");
                context.Comments.Remove(data);

                //await context.SaveChangesAsync();
                return Results.Ok();
            }
            #endregion

            return Results.Forbid();
        }

    }
}
