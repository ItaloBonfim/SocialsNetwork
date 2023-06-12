using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.DTO.Class
{
    public record FriendRequestResponse(Guid requestId, string id, string UserName, string email, string avatarURL, RequestStatus Status);
}
