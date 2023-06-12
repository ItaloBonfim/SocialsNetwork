using SocialsNetwork.DTO.Streams;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.StreamSpace;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Streams.ChannelBlocks
{
    public class ChannelBlocklistPost
    {
        public static string Template => "api/channel/blocklist/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(ChannelBlocklistRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            /*
             * usuario logado
             * id do usuario a ser bloqueado
             * comparar se o usuario logado é o dono do canal
             * 
             * inserir dados
             */

            var Query =
               (from CBLC in context.ChannelBlocklist
                join STMC in context.StreamChennels on CBLC.IdChannel.Id equals STMC.Id
                join aspUsers in context.ApplicationUsers on CBLC.UserId equals aspUsers.Id
                where
                 STMC.Id == request.channelId
                 
                select new
                {
                    ChannelId = STMC,
                    ADM = STMC.UserId,
                    
                }).FirstOrDefault();

            if(Query != null)
            {
                if (Query.ChannelId == null)
                    return Results.NotFound("Channel not found");

                if (Query.ADM != LoggedUser)
                    return Results.BadRequest("Not avaliable to this user");
            }

            var usersBlocked = context.ApplicationUsers.FirstOrDefault(x => x.Id == request.userBlockId);

            if (usersBlocked == null)
                return Results.NotFound("Usuario não identificado");

            var data = new ChannelBlocklist(Query.ChannelId, usersBlocked, request.motivation);
            if (!data.IsValid)
                return Results.BadRequest("Failure when try create objetect");
        
            return Results.Created("channelBlocklist/{id}", data.Id);
        }
    }
}
