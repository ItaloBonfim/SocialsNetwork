using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.DTO.Class
{
    public record Friends (Guid FriendshipId, string AskFriendshipId,
                            string AskedId, string name, string email, string avatarURL, DateTime createdOn); //retornar todos os amigos
    public record FriendInvite(string Invited); // novas solicitações de amizade
    public record FriendInviteManager(Guid Id, string AskFriendship, string Asked, RequestStatus status); // gerenciar convites de amizade (aceitar, negar)
    public record FriendsInviteReceived(Guid FriendRequestId, string User, int RequestStatus, string ByUser, string RequesterEmail, string Name, string RequestAvatarURL, DateTime CreatedOn); // obter todas as solicitações de amizades recebidas
    public record FriendsRequetsMade(Guid FriendRequestId, string ByUser, int RequestStatus, string ToUser, string RequesterEmail, string Name, string RequestAvatarURL, DateTime CreatedOn); // obter todas solicitações de amizades realizadas



}
