namespace SocialsNetwork.DTO.Class
{
    public record FriendshipsResponse
        (Guid FriendshipID, string UserId, string AskFriendshipId,
        string AskedId, string name, string email, string avatarURL, DateTime createdOn);
       

}
