using SocialsNetwork.Models.Socials.Enums;

namespace SocialsNetwork.DTO.Socials
{
    public record ReactionPublicationRequest(Guid Publication, int Reaction);
    public record ReactionPublicationResponse(string rUserName, string rUserId, Guid PublicationId, string UserPublisherId, DateTime CreatedOn, string UserPublisherName, Guid Id, int TypeReactionFK, int UserExpression );
    public record NewReactionComment(Guid Comment, int Reaction);
    public record NewReactionSubComment(Guid Comment, string User, int reaction);
    public record ReactionResponseComment(Guid Id, string Comment, string type, Guid User, string Publcation, string AvatarURL, string Name);
    public record ReactionResponseSubComment(Guid Id, string Comment, string type, Guid User, string Publcation, string AvatarURL, string Name, int QtdReactions);
    public record TypesReactions (int Id, string Description);
        
}
