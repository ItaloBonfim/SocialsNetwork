using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.StreamSpace;
using System.Security.Claims;
using SocialsNetwork.Models.StreamSpace.Enums;
using SocialsNetwork.DTO.Streams;

namespace SocialsNetwork.Endpoints.Streams.CHConfigurations
{
    public class ChannelConfigurationsPost
    {
        public static string Template => "";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(
            ChannelConfigurationsRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var Query =
               (from CHCONF in context.ChannelConfigurations
                join STMC in context.StreamChennels on CHCONF.Channel.Id equals STMC.Id
                join aspUsers in context.ApplicationUsers on STMC.UserId equals aspUsers.Id

                where
                 STMC.Id == request.ChannelId

                select new
                {
                    ChannelId = STMC,
                    ADM = aspUsers,

                }).FirstOrDefault();

            if (Query == null)
                return Results.NotFound("Channel, not found");

            if (Query.ADM.Id != LoggedUser)
                return Results.Forbid();

            var data = new ChannelConfiguration(
                "",
                EnableProperty.Desativado,
                EnableProperty.Desativado,
                EnableProperty.Ativo,
                "Alert Mensage",
                "PT-BR",
                EnableProperty.Ativo,
                "",
                3,
                EnableProperty.Ativo,
                5.00,
                Query.ChannelId);

            context.ChannelConfigurations.Add(data);
            context.SaveChanges();


            return Results.Ok();
        }
    }
}
