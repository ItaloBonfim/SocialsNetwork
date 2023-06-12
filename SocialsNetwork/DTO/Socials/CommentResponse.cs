namespace SocialsNetwork.DTO.Socials
{
    public class CommentResponse
    {
        public Guid User { get; set; }
        public string Email { get; set; }
        public string AvatarURL { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Text { get; set; }
        public string ImageURL { get; set; }
        public string MidiaURL { get; set; }
        public Guid Publication { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
