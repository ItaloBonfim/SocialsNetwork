using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Blocklists
{
    public class BlocklistPost
    {
        public static string Template => "api/blocklist/block/{id}";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action([FromRoute]string id, HttpContext http, AppDbContext context, FindUserAndReturnAll query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
   
            var user = query.Execute(LoggedUser).Result;
            var blockedUser = query.Execute(id).Result;


            var dataRegister = await (from X in context.BlockLists
                            where (X.User.Id == user.Id && X.Blocked.Id == blockedUser.Id) || (X.Blocked.Id == user.Id && X.User.Id == blockedUser.Id)
                            select new { Id = X.Id }).FirstOrDefaultAsync(); 
          
           
            if(dataRegister != null)
            {
                return Results.BadRequest("Já existe um registro cadastrado com esses usuarios...");
            } 

            var response = new BlockList(user, blockedUser);
            await context.BlockLists.AddAsync(response);
            await context.SaveChangesAsync();

            return Results.Created($"/blocklist/{response.Id}", response.Id);
        }


    }
}
