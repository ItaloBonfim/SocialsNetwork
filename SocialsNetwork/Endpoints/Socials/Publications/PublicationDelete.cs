using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Publications
{
    public class PublicationDelete
    {
        public static string Template => "api/publications/Delete/{publicationId:Guid}";
        public static string[] Methods => new string[] { HttpMethod.Delete.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action([FromRoute] Guid publicationId, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var data = await context.Publication.FirstOrDefaultAsync(
                x => x.Id == publicationId && x.UserId == LoggedUser);
            if (data == null)
                return Results.NotFound("Publicação não identificada");

            //Um usuario só pode apagar uma publicação que ele criou
            if (!data.UserId.Equals(LoggedUser)) return Results.Forbid();

            context.Publication.Remove(data);
            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
