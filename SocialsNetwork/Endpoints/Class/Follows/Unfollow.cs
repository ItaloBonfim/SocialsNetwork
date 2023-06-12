using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Follows
{
    public class Unfollow
    {
        public static string Template => "api/follows/unfollow/{IdFollow}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;
        public static async Task<IResult> Action([FromRoute] Guid IdFollow, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var Query = await (from FLL in context.Follows
                         join aspUsers in context.ApplicationUsers on FLL.User.Id equals aspUsers.Id
                         where 
                         aspUsers.Id == LoggedUser
                         && FLL.FollowedUser.Id == IdFollow.ToString()
                         select new
                         {
                            Id = FLL.Id,
                            User = FLL.User,
                            Followed = FLL.FollowedUser,
                            CreatedOn = FLL.CreatedOn
                         }).FirstOrDefaultAsync();

            if (Query == null)
                return Results.NotFound("Registro não identificado");

            Follow register = new Follow
            {
                Id = Query.Id,
                User = Query.User,
                FollowedUser = Query.Followed,
                CreatedOn = Query.CreatedOn
            };

            context.Follows.Remove(register);
            await context.SaveChangesAsync();
           

            return Results.Ok();
        }
    }
}
