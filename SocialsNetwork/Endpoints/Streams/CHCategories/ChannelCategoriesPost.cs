using SocialsNetwork.DTO.Streams;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.StreamSpace;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Streams.CHCategories
{
    public class ChannelCategoriesPost
    {
        public static string Template => "";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(ChannelcategoriesRequest request,HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var channel = context.StreamChennels.FirstOrDefault(x => x.Id == request.channelId);
            if (channel == null) return Results.NotFound();
            
            var categorie = context.StreamingCategories.FirstOrDefault(x => x.Id == request.categorieId);
            if(categorie == null) return Results.NotFound();

            if (channel.User.Id != LoggedUser)
                return Results.Forbid();

            var data = new ChannelCategories(channel, categorie);
            if (!data.IsValid)
                return Results.BadRequest("Failure on create object");

            return Results.Created("ChannelBlocklist/{id}", data.Id);
        }
    }
}
