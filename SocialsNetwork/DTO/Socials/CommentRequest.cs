namespace SocialsNetwork.DTO.Socials
{
    public class CommentRequest
    {
        public string userId { get; set; }
        public Guid PublicationId { get; set; }
        public string CommentValue { get; set; }
        public string? ImageURL { get; set; }
        public string? MidiaURL { get; set; }
    }
}
