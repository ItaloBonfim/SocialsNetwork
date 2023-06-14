using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Endpoints.DicionaryErrors;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Class.FriendRequests
{
    public class NewFriendFriendRequest
    {
        public static string Template => "api/friendRequests/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public static async Task<IResult> Action(FriendInvite request, HttpContext http, AppDbContext context, FindUserAndReturnAll Query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            if (request.Invited.Equals(LoggedUser)) return Results.BadRequest();

            var userRequester = Query.Execute(LoggedUser).Result;
            if (userRequester == null) return Results.BadRequest("Usuario solicitante não identificado...");

            var userInvited = Query.Execute(request.Invited).Result;
            if (userInvited == null) return Results.BadRequest("Usuario solicitado não identificado...");

            var BlackList = await (from BLS in context.BlockLists
                                  where
                                  ( BLS.User.Id == userRequester.Id && BLS.Blocked.Id == userInvited.Id )
                                  || ( BLS.Blocked.Id == userRequester.Id && BLS.User.Id == userInvited.Id )
                                  select new { Id = BLS.Id }
                                  ).FirstOrDefaultAsync();

            if (BlackList != null) return Results.BadRequest("Existem retrições entre esses usuarios...");

            var Friendship = await (from FSP in context.Friendships
                                    where
                                    (FSP.AskFriendship.Id == userRequester.Id && FSP.Asked.Id == userInvited.Id)
                                    || (FSP.Asked.Id == userRequester.Id && FSP.AskFriendship.Id == userInvited.Id)
                                    select new { Id = FSP.Id }
                                    ).FirstOrDefaultAsync();

            if (Friendship != null) return Results.BadRequest("Já Existe uma amizade entre os usuarios...");

            var register = await (from FRS in context.FriendRequests
                                  where 
                                  ( FRS.AskFriendship.Id == userRequester.Id && FRS.Asked.Id == userInvited.Id )
                                  || ( FRS.Asked.Id == userRequester.Id && FRS.AskFriendship.Id == userInvited.Id )
                                  select new {Id = FRS.Id}
                                  ).FirstOrDefaultAsync();

            if (register != null) return Results.BadRequest("Ja existe registro dessa solicitação cadastrada...");

            

            var data = new FriendRequest(userRequester, userInvited);

            if (!data.IsValid) return Results.ValidationProblem(data.Notifications.convertToDetails());

            await context.FriendRequests.AddAsync(data);
            await context.SaveChangesAsync();


            return Results.Created($"/Request/{data.Id}", data.Id);
        }

    }
}
