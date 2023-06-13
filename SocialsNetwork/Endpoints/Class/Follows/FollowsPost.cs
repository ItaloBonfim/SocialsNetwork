using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Follows
{
    public class FollowsPost
    {
        public static string Template => "api/follows/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action(doFollow request, HttpContext http, AppDbContext context, FindUserAndReturnAll query)
        {

            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (LoggedUser.Equals(request.UserId))
                return Results.BadRequest();


            var user = query.Execute(LoggedUser).Result;
            var followedUser = query.Execute(request.UserId).Result;
            var registerData = await (from reg in context.Follows where reg.User.Id.Equals(user.Id) && reg.FollowedUser.Id.Equals(followedUser.Id) select new {Id = reg.Id}).FirstOrDefaultAsync();
            
            if(registerData != null) return Results.BadRequest("Já existe registro com esses usuarios...");

            if (user == null || followedUser == null)
                return Results.NotFound();

            var data = new Follow(user, followedUser);
            await context.Follows.AddAsync(data);
            await context.SaveChangesAsync();

            return Results.Created($"Follows/new/{data.Id}", data.Id);
        }
    }
}
