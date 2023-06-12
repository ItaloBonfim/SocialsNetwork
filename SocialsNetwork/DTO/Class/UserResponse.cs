namespace SocialsNetwork.DTO.Class
{
    public record UserResponse(Guid Id, string UserId, string ClaimValue, string Email, string AvatarURL);

    public record UserResponseBlock(Guid Id, string UserId, string ClaimValue, string Email, string AvatarURL, string BlockedId);
   
}
