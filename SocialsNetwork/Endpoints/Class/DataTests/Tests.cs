using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Endpoints.Class.DataTests
{
    public class Tests
    {
        public static string Template => "/TESTER";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

         public static async Task<IResult> Action(UserManager<ApplicationUser> manager)
         {
             //var data = context.ApplicationUsers.Where(x => x.Dis == "IdentityUser" &&  x.Id == "098907f5-6f85-4c46-8f0d-7da8e62a9bed").FirstOrDefault();
             var data = await manager.FindByIdAsync("a0bb4117-7762-492c-8137-d4452a487503");
             return Results.Ok(data);

             //FUNCIONOU
         } 

        /* public static IResult Action(UserManager<IdentityUser> manager)
         {
             //var data = context.ApplicationUsers.Where(x => x.Dis == "IdentityUser" &&  x.Id == "098907f5-6f85-4c46-8f0d-7da8e62a9bed").FirstOrDefault();
             var data = manager.FindByIdAsync("098907f5-6f85-4c46-8f0d-7da8e62a9bed");
             return Results.Ok(data);
             //ERRO 500
         } */

        /* public static async Task<IResult> Action(FindUserAndReturnAll query, UserManager<ApplicationUser> manager)
        {
            //var data = context.ApplicationUsers.Where(x => x.Dis == "IdentityUser" &&  x.Id == "098907f5-6f85-4c46-8f0d-7da8e62a9bed").FirstOrDefault();
            var data = query.Execute("098907f5-6f85-4c46-8f0d-7da8e62a9bed");
            return Results.Ok(data);

            // NEW COMPILA */
    }
    
}

