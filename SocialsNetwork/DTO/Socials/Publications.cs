namespace SocialsNetwork.DTO.Socials
{
    public record Publications();
    public record PublicationRequest(string? ImageURL, string? MidiaURL, string? TextValue);
    public record HomePublications(string User, string Name, string AvatarURL, Guid Publication, string Content, string ImageURL, string MidiaURL, DateTime CreatedOn, DateTime UpdateOn, int QtdComments, int QtdReactions);
    public record PublicationResponse(string User, string Name, string AvatarURL, string PublicationId, string ImageURL, string MidiaURL, string TextValue, DateTime CreatedOn, DateTime UpdateOn, int QtdComments, int QtdReactions);

  
    
}
