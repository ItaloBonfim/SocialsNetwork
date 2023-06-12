namespace SocialsNetwork.DTO.Socials
{
    public class CommentReactionRequest
    {
        public string? Id { get; set; }
        public Guid? pCommentId { get; set; }
        public Guid? sCommentId { get; set; }
        public string User { get; set; }
        public int ReactType { get; set; }

    }
}
