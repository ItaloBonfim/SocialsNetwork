using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using SocialsNetwork.Models.Socials.Enums;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Publication
{
    public class ReactionPost
    {
        public static string Template => "api/reaction/publication/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody] ReactionPublicationRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var Exists = await (from R in context.Reaction
                                where
                                R.Publication.Id == request.Publication && R.UserId == LoggedUser
                                select new { R }).FirstOrDefaultAsync();

            if (Exists != null) return Results.BadRequest("Por Favor utilize outro metodo para atualizar sua reação.");

            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            if (user == null) return Results.NotFound();

            var Query = await (from P in context.Publication
             where P.Id == request.Publication
             select new
             {
                 P
             }).FirstOrDefaultAsync();

            if(Query == null) return Results.NotFound("Publicação não encontrada.");

            var TypeReact = await (from TR in context.TypeReactions
                                   where TR.Id == request.Reaction
                                   select new
                                   {
                                       TR
                                   }).FirstOrDefaultAsync();

            if (TypeReact == null) return Results.NotFound("Tipo de Reação não Encontrado.");

            var data = new Reaction(Query.P, user, TypeReact.TR);

            if (!data.IsValid)
                return Results.BadRequest();

            await context.Reaction.AddAsync(data);
            await context.SaveChangesAsync();

            return Results.Ok();
        }
    }
}
