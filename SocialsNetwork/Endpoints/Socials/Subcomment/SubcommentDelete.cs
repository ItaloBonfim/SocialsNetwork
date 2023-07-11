using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using System.ComponentModel.Design;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Subcomment
{
    public class SubcommentDelete
    {
        public static string Template => "api/sub/comments/delete/{sCommenteId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action([FromRoute] Guid sCommenteId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            #region BUSCA PELA RESPOSTA AO COMENTARIO ATRAVES DA ARVORE
            var pcs = await (
                        from P in context.Publication
                        join C in context.Comments on P.Id equals C.Publication.Id
                        join S in context.SubComments on C.Id equals S.Comment.Id
                        where
                        S.Id == sCommenteId
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

            #endregion


            #region EXCLUSÃO DE DADOS POR MEIO DE VALIDAÇÃO DO USUARIO LOGADO
            // AUTOR DA PUBLICAÇÃO 
            if (pcs.pAutor.Equals(LoggedUser) && pcs.sAutor.Equals(LoggedUser))
            {

            }

            if(pcs.pAutor.Equals(LoggedUser) && !pcs.sAutor.Equals(LoggedUser))
            {

            }

            // AUTOR DE UM COMENTARIO 

            if(pcs.cAutor.Equals(LoggedUser) && pcs.sAutor.Equals(LoggedUser))
            {

            }

            if(pcs.cAutor.Equals(LoggedUser) && !pcs.sAutor.Equals(LoggedUser))
            {

            }

            // AUTOR DE UMA RESPOSTA
            if(!pcs.cAutor.Equals(LoggedUser) && pcs.sAutor.Equals(LoggedUser))
            {

            }
            #endregion



            return Results.Ok();
        }
    }
}
