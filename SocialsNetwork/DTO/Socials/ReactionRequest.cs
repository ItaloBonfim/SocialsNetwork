namespace SocialsNetwork.DTO.Socials
{
    public class ReactionRequest
    {
        public Guid Id { get; set; }
        
        public string PublicationId { get; set; }

        public string UserId { get; set; }

        public string reaction { get; set; }

    }
}
