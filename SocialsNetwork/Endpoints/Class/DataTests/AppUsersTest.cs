using Microsoft.AspNetCore.Identity;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Endpoints.Class.DataTests
{
    public class AppUsersTest
    {
        public static string Template => "/TESTER/APPUSERS";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action(UserManager<ApplicationUser> manager)
        {
           var x = await manager.FindByIdAsync("aksdçkasdçaksd");
            
            

            return Results.Ok();
        }
    }
}
