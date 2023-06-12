using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Models.Socials
{
    public class Comment : Notifiable<Notification>
    {
        public Comment() { }
        public Guid Id { get; set; }
        public Publication Publication { get; set; } // pertence a uma publicação
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string CommentValue { get; set; }
        public string? ImageURL { get; set; }
        public string? MidiaURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<SubComment> subComments { get; set; } // tem N subcomentarios
        public List<CommentReaction> CommentsReactions { get; set; } // tem N reações 
        public Comment(
            Publication publication, ApplicationUser user, string commentValue, string? imageURL, string? midiaURL)
        {
            var contract = new Contract<Comment>()
                .IsNotNull(publication, "Publication")
                .IsNotNull(user, "User")
                .IsNotNull(commentValue, "CommentValue")
                .IsNotNull(imageURL, "ImageURL")
                .IsNotNull(midiaURL, "MidiaURL");
            AddNotifications(contract);

            Id = new Guid();
            Publication = publication;
            User = user;
            CommentValue = commentValue;
            ImageURL = imageURL;
            MidiaURL = midiaURL;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
        }
    }
}
