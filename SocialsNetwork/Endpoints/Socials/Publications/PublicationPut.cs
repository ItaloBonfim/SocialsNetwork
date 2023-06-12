﻿using SocialsNetwork.Infra.Data;
using System.Security.Claims;

namespace SocialsNetwork.Endpoints.Socials.Publications
{
    public class PublicationPut
    {
        public static string Template => "api/publications/update";
        public static string[] Methods => new string[] { HttpMethod.Get.ToString() };
        public static Delegate Handle => Action;

        public static IResult Action(HttpContext http, AppDbContext context)
        {
            var LoggedUser = http.User.Claims.First(c => c.Type == ClaimTypes.NameIdentifier).Value;


            return Results.Ok();
        }
    }
}
