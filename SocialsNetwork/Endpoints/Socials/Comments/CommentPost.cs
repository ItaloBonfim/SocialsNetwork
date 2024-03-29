﻿using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Socials;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Socials;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Comments
{
    public class CommentPost
    {
        public static string Template => "api/comments/new";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public async static Task<IResult> Action([FromBody]CommentRequest request, HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;
            if (LoggedUser == null)
                return Results.Forbid();
            var user = await context.ApplicationUsers.FindAsync(LoggedUser);
            //var pub = await context.Publication.FindAsync(request.PublicationId);
            var pub = context.Publication.FirstOrDefault(
                        x => x.Id == request.PublicationId);

            if (user == null || pub == null)
                return Results.NotFound("Informações necessarias não identificadas");

           var data =
                new Comment(pub, user, request.CommentValue, request.ImageURL, request.MidiaURL);
            if (!data.IsValid)
                return Results.BadRequest("Falha na validação");

            await context.Comments.AddAsync(data);
            await context.SaveChangesAsync();
            return Results.Ok(data.Id);
        }
    }
}
