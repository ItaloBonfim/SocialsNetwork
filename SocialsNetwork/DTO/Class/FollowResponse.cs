namespace SocialsNetwork.DTO.Class
{
    public record doFollow(int? Id, string UserId);
    public record FilterFollows(string? userId, string? filter);
    public record FollowResponse
        (string UserId, string Name, string Email, string AvatarURL, string? Follow, DateTime CreatedOn);


}
