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
        public static IResult Action(UserRequest userRequest, UserManager<ApplicationUser> userManager)
        {
            //var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            /* var user = new IdentityUser
             {
                 UserName = userRequest.Email,
                 Email = userRequest.Email,
                 PhoneNumber = userRequest.PhoneNumber,
             }; */
            var user = new ApplicationUser(userRequest.BirthDate, userRequest.genre)
            {
                UserName = userRequest.Email,
               // UserName = userRequest.Name,
                Email = userRequest.Email,
                PhoneNumber = userRequest.PhoneNumber,

            };

            var result = userManager.CreateAsync(user, userRequest.Password).Result;

            if (!result.Succeeded)
                return Results.ValidationProblem(result.Errors.convertToDetails());

            var userClaims = new List<Claim>
            {
                //new Claim(ClaimTypes.Email, userRequest.Name),
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim("Name", userRequest.Name)
                
            };
            var claimResult =
                 userManager.AddClaimsAsync(user, userClaims).Result;

            if(!claimResult.Succeeded)
                return Results.BadRequest(claimResult.Errors.First());

            return Results.Created($"/user/{user.Id}", user.Id);
        }
    }
}

