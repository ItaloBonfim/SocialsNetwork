using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.DTO.Class
{
    public record Friends (Guid FriendshipID, string UserId, string AskFriendshipId,
                            string AskedId, string name, string email, string avatarURL, DateTime createdOn); //retornar todos os amigos
    public record FriendInvite(string Invited); // novas solicitações de amizade
    public record FriendInviteManager(Guid Id, string AskFriendship, RequestStatus status); // gerenciar convites de amizade (aceitar, negar)
    public record FriendsInviteResponse(Guid FriendRequestId, string Requester, int RequestStatus, string RequesterId, string RequesterEmail, string Name, string RequestAvatarURL, DateTime CreatedOn); // obter todas as solicitações de amizades



}
