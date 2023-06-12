using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SocialsNetwork.Models.Class;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SocialsNetwork.Endpoints.Security
{
    public class TokenPost
    {
        public static string Template => "api/token";
        public static string[] Methods => new string[] { HttpMethod.Post.ToString() };
        public static Delegate Handle => Action;
        [AllowAnonymous]
        public static IResult Action(LoginRequest login,IConfiguration configuration, UserManager<ApplicationUser> manager)
        {
            var user = manager.FindByEmailAsync(login.Email).Result;
            if (user == null)
                Results.BadRequest();
            if (!manager.CheckPasswordAsync(user, login.Password).Result)
                Results.BadRequest();

            var claims = manager.GetClaimsAsync(user).Result;
            var Subject = new ClaimsIdentity(new Claim[]
                {
                     new Claim(ClaimTypes.Email, login.Email),
                     new Claim(ClaimTypes.NameIdentifier, user.Id)
                });
            Subject.AddClaims(claims);

            var key = Encoding.ASCII.GetBytes(configuration["JwtBearerTokenSettigns:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = Subject,
                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Audience = configuration["JwtBearerTokenSettigns:Audience"],
                Issuer = configuration["JwtBearerTokenSettigns:Issuer"],
                Expires = DateTime.UtcNow.AddHours(1)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            
            return Results.Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}
