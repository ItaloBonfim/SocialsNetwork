using Microsoft.AspNetCore.Mvc;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;

namespace SocialsNetwork.Endpoints.Class.Users
{
    public class UserPut
    {
        public static string Template => "api/user/{id}";
        public static string[] Methods => new string[] { HttpMethod.Put.ToString() };
        public static Delegate Handle => Action;
        public static IResult Action([FromRoute] Guid id, UserRequest userRequest, AppDbContext context)
        {



            return Results.Ok();
        }
    }
}
