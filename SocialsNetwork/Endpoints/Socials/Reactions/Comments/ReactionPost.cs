﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Reactions.Comments
{
    public class ReactionPost
    {
        public static string Template => "api/Reaction/comment/new";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody] NewReactionComment request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;

            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            if (user == null) return Results.BadRequest();

            var Existente = await (from X in context.CommentReactions
                                   where
                                   X.Comment.Id == request.Comment
                                   && X.UserId == user.Id
                                   select new { X }).FirstOrDefaultAsync();
            if(Existente != null) return Results.BadRequest("Por Favor utilize outro metodo para atualizar sua reação");

            var Query = await (from C in context.Comments
                               where C.Id == request.Comment
                               select new { C }).FirstOrDefaultAsync();
            if(Query == null) return Results.BadRequest();

            var TypeReact = await (from TR in context.TypeReactions
                                   where TR.Id == request.Reaction
                                   select new
                                   {
                                       TR
                                   }).FirstOrDefaultAsync();

            if (TypeReact == null) return Results.NotFound("Tipo de Reação não Encontrado.");

            var data = new CommentReaction(Query.C, user, TypeReact.TR);
            if (!data.IsValid)
                return Results.BadRequest();

            await context.CommentReactions.AddAsync(data);
            await context.SaveChangesAsync();

            return Results.Ok();
        }

    }
}
