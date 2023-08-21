using Microsoft.AspNetCore.Identity;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Endpoints.DicionaryErrors;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Users
{
    public class UserPost
    {
        public static string Template => "api/user";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;
        public static IResult Action(NewUser userRequest, UserManager<ApplicationUser> userManager)
        {


            var user = new ApplicationUser(userRequest.BirthDate, userRequest.Genre)
            {
                UserName = userRequest.Email,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,
            };

            var result = userManager.CreateAsync(user, userRequest.Password).Result;

            if (!result.Succeeded)
                return Results.ValidationProblem(result.Errors.convertToDetails());

            var userClaims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Name", userRequest.Name),
                new Claim("Username", "@"+userRequest.Username)
                
            };
            var claimResult =
                 userManager.AddClaimsAsync(user, userClaims).Result;

            if(!claimResult.Succeeded)
                return Results.BadRequest(claimResult.Errors.First());

            return Results.Created($"/user/{user.Id}", user.Id);
        }
    }
}

