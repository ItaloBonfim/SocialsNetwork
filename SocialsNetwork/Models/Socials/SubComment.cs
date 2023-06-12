using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Models.Socials
{
    public class SubComment : Notifiable<Notification>
    {
        public SubComment() { }
        public Guid Id { get; set; }
        public Comment Comment { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Text { get; set; }
        public string ImageUrl { get; set; }
        public string MidiaUrl { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }

        public SubComment(Comment comment, ApplicationUser user, string text, string imageUrl, string midiaUrl)
        {
            var contract = new Contract<SubComment>()
                .IsNotNull(comment, "Comment")
                .IsNotNull(user, "User")
                .IsNotNull(text, "Text")
                .IsNotNull(imageUrl, "ImageURL")
                .IsNotNull(midiaUrl, "MidiaURL");
            AddNotifications(contract);

            Comment = comment;
            User = user;
            Text = text;
            ImageUrl = imageUrl;
            MidiaUrl = midiaUrl;
            CreatedOn = DateTime.Now;
            UpdatedOn = DateTime.Now;
            
        }
    }
}
