using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Business.Class.Bloqueios;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Interfaces.Class.Business;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Blocklists
{
    public class BlocklistDelete
    {
        public static string Template => "api/blocklist/delete/{IdBlock}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute]Guid IdBlock, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            IBlock methods = new ControleBloqueio();

            //if (LoggedUser == null)
            //    return Results.Forbid();

            //var Query = await (from BLC in context.BlockLists
            //             join aspUsers in context.ApplicationUsers on BLC.User.Id equals aspUsers.Id
            //             where
            //             aspUsers.Id == LoggedUser && BLC.Id == IdBlock
            //             select new
            //             {
            //                 Id = BLC.Id,
            //                 User = BLC.User,
            //                 Blocked = BLC.Blocked,
            //                 CreatedOn = BLC.CreatedOn
            //             }).FirstOrDefaultAsync();

            //if (Query == null) return Results.NotFound();

            //BlockList register = new BlockList
            //{
            //    Id = Query.Id,
            //    User = Query.User,
            //    Blocked = Query.Blocked,
            //    CreatedOn = Query.CreatedOn,
            //};

            //context.BlockLists.Remove(register);
            //await context.SaveChangesAsync();

            if (methods.VerificarExistencia(context,IdBlock.ToString(), LoggedUser)) return Results.NotFound();

            methods.RemoverUsuarioBloqueado(context, LoggedUser, IdBlock);


            return Results.Ok();
        }
    }
}
