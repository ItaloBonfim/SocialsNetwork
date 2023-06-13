using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.DTO.Class
{
    public record UserData(Guid Id, string UserId, string ClaimValue, string Email, string AvatarURL);
    public record UserResponse(Guid Id, string UserId, string ClaimValue, string Email, string AvatarURL);
    public record UserResponseBlock(Guid Id, string UserId, string ClaimValue, string Email, string AvatarURL, string BlockedId);
    public record NewUser(string Email, string Password, string Name, DateTime BirthDate, Genre Genre, string PhoneNumber);
    public record UpdateInfo (string Name, string Password, string AvatarURL, DateTime BirthDate, Genre Genre, string PhoneNumber);
   
}
