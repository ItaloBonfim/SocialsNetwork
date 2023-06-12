namespace SocialsNetwork.DTO.Socials
{
    public class SubcommentRequest
    {
        public string Id { get; set; }
        public Guid pCommentId { get; set; }
        public string user { get; set; }
        public string CommentValue { get; set; }
        public string ImageURL { get; set; }
        public string MidiaURL { get; set; }
    }
}
