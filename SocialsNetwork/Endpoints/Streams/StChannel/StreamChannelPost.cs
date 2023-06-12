using SocialsNetwork.Infra.Data;
using System.Security.Claims;
using SocialsNetwork.Models.StreamSpace;
using SocialsNetwork.Models.StreamSpace.Enums;
using SocialsNetwork.DTO.Streams;

namespace SocialsNetwork.Endpoints.Streams.StChannel
{
    public class StreamChannelPost
    {
        public static string Template => "";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(StreamChannelRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();

            var ADM = context.ApplicationUsers.FirstOrDefault(x => x.Id == LoggedUser);
            if (ADM == null)
                return Results.BadRequest("Usuario não localizado");

            var Channel = new StreamChannel(
                request.ChannelName,
                request.AvatarURL,
                request.ArtURL,
                ADM
                //Configuration
                );

            if (!Channel.IsValid)
                return Results.BadRequest("Erro ao configurar stream Channel");


            var Configuration = ConfigureChannel(Channel);
            if (!Configuration.IsValid)
                return Results.BadRequest();

            Channel.ConfigurationId = Configuration.Id;
            Configuration.Channel = Channel;

           // context.StreamChennels.Add(Channel);
           // context.ChannelConfigurations.Add(Configuration);

            context.AddRange(Channel, Configuration);

            context.SaveChanges();


            return Results.Ok();
        }

        private static ChannelConfiguration ConfigureChannel(StreamChannel channel)
        {
            return  new ChannelConfiguration(
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
                channel);
            

        }
    }
}
