using Microsoft.AspNetCore.Identity;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Endpoints.Class.Users
{
    public class UserPut
    {
        public static string Template => "api/user";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;
        public static IResult Action(UpdateInfo userRequest, UserManager<ApplicationUser> userManager)
        {



            return Results.Ok();
        }
    }
}
