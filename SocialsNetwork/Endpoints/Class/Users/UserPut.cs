using Microsoft.AspNetCore.Identity;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Models.Class;
using static System.Net.WebRequestMethods;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.Users
{
    public class UserPut
    {
        public static string Template => "api/user";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;
        public static async Task<IResult> Action(UpdateInfo userRequest, HttpContext http, UserManager<ApplicationUser> userManager)
        {
            //https://stackoverflow.com/questions/24587414/how-to-update-a-claim-in-asp-net-identity
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = userManager.FindByIdAsync(LoggedUser).Result;
            if (user == null) return Results.NotFound();

            var result = user;
            


            await userManager.UpdateAsync(user);
            return Results.Ok();
        }
    }
}
