using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Friends
{
    public class FriendshipsDelete
    {
        public static string Template => "/friendships/delete/{Id:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromRoute] Guid Id, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            
            var data = await (from FRS in context.Friendships
                              where
                              (FRS.Id.Equals(Id))
                              && (FRS.AskFriendship.Id.Equals(LoggedUser) || FRS.Asked.Id.Equals(LoggedUser))
                              select new Friendships
                              {
                                  Id = FRS.Id,
                                  AskFriendship = FRS.AskFriendship,
                                  Asked = FRS.Asked,
                                  CreatedOn = FRS.CreatedOn,
                              }).FirstOrDefaultAsync();

            if (data == null) return Results.NotFound("Dados de registro não encontrados na tabela!");

            await context.SaveChangesAsync();   
            return Results.Ok();
        }
    }
}
