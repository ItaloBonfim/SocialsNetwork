namespace SocialsNetwork.DTO.Socials
{
    public class CommentReactionResponse
    {
        public Guid  Id { get; set; }
        public string CommentFK { get; set; }
        public Guid CommentId { get; set; }
        public string Type { get; set; }
        public Guid User { get; set; }
        public string Publication { get; set; }
        public string AvatarURL { get; set; }
        public string Name { get; set; }
    }
}
