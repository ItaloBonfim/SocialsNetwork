using SocialsNetwork.Models.Socials.Enums;

namespace SocialsNetwork.DTO.Socials
{
    public record ReactionPublicationRequest(Guid Publication, int Reaction);
    public record ReactionPublicationResponse(string Id, string Reaction, string Description, string  User, string AvatarURL, string Name, DateTime CreatedOn);
    public record NewReactionComment(Guid Comment, int Reaction);
    public record NewReactionSubComment(Guid Comment, string User, int reaction);
    public record ReactionResponseComment(Guid Id, string Comment, string type, Guid User, string Publcation, string AvatarURL, string Name);
    public record ReactionResponseSubComment(Guid Id, string Comment, string type, Guid User, string Publcation, string AvatarURL, string Name, int QtdReactions);
    public record TypesReactions (int Id, string Description);
        
}
