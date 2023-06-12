using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Friends
{
    public class FriendshipsDelete
    {
        public static string Template => "/friendships/delete/{Id:guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute] Guid Id, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var QueryJoin = await (from FRR in context.Friendships
                     join aspUsers in context.ApplicationUsers on FRR.AskFriendship.Id equals aspUsers.Id

                             where aspUsers.Id == LoggedUser || FRR.Asked.Id == LoggedUser
                             select new
                     {
                         Id = FRR.Id,
                         askFriendship = FRR.AskFriendship,
                         asked = FRR.Asked,
                         CreatedOn = FRR.CreatedOn
                     }).FirstOrDefaultAsync();

            if (QueryJoin == null)
                return Results.NotFound("Registro não identificado");

            Friendships RemoveData = new Friendships {
                Id = QueryJoin.Id,
                AskFriendship = QueryJoin.askFriendship,
                Asked = QueryJoin.asked,
                CreatedOn = QueryJoin.CreatedOn,
            };


            context.Friendships.Remove(RemoveData);
            //await context.SaveChangesAsync();


            if (QueryJoin == null)
                return Results.NotFound("Não idenficado alguma coisa");

            var askFriendship = QueryJoin.askFriendship.Id;
            var asked = QueryJoin.asked.Id;

            
            return Results.Ok();
        }
    }
}
