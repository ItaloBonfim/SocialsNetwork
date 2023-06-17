using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Publications
{
    public class PublicationPost
    {
        public static string Template => "api/publications/new";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody] PublicationRequest request,HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            if (user == null)
                return Results.BadRequest("Usuario não identificado");

            var data = new Publication(user, request.TextValue, request.ImageURL, request.MidiaURL);
            if (!data.IsValid)
                return Results.BadRequest("Falha na validação");
            await context.Publication.AddAsync(data);
            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
