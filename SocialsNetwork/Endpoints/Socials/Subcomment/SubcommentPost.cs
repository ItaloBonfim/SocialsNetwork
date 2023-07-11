using Azure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Subcomment
{
    public class SubcommentPost
    {
        public static string Template => "api/sub/comments/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;
        public async static Task<IResult> Action([FromBody] newSubcomment request, HttpContext http, AppDbContext context, FindUserAndReturnAll query)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = query.Execute(LoggedUser).Result;
            if (user == null) return Results.NotFound();

            //var primary = await (from X in context.Comments
            //                     where
            //                     X.Id.Equals(request.CommentId)
            //                     select new
            //                     {
            //                         Id = X.Id,
            //                         UserId = X.UserId,
            //                         CommentValue = X.CommentValue,
            //                         ImageURL = X.ImageURL,
            //                         MidiaURL = X.MidiaURL,
            //                         CreatedOn = X.CreatedOn,
            //                         UpdatedOn = X.UpdatedOn
            //                     }).FirstOrDefaultAsync();
            //if(primary == null) return Results.NotFound();
            var primary = await context.Comments.SingleAsync(x => x.Id.Equals(request.CommentId));
            if (primary == null) return Results.NotFound();

            var secondary = new SubComment(primary, user, request.Comment, request.ImageURL, request.MidiaURL);
            if(!secondary.IsValid) return Results.BadRequest();

            await context.SubComments.AddAsync(secondary);
            await context.SaveChangesAsync();

            return Results.Created($"/sub/comments/{secondary.Id}", secondary.Id);
        }
    }
}
