namespace SocialsNetwork.DTO.Socials
{
    public class PublicationResponse
    {
        public string User { get; set; }
        public string Name { get; set; }
        public string AvatarURL { get; set; }
        public string Publication { get; set; }
        public string Content { get; set; }
        public string ImageURL { get; set; }
        public string MidiaURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
