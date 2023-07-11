using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;
using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.Models.Socials
{
    
    public class Publication : Notifiable<Notification>
    {
        public Publication() { }
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string? Text { get; set; }
        public string? ImageURL { get; set; }
        public string? MidiaURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public List<Comment> PublicationComments { get; set; }
        public List<Reaction> Reactions { get; set; }

        public Publication(ApplicationUser user, string? text, string? imageURL, string? midiaURL)
        {
            User = user;
            Text = text;
            ImageURL = imageURL;
            MidiaURL = midiaURL;
            CreatedOn = DateTime.Now;
            DataValidate();
        }

        public void DataValidate()
        {
            var contract = new Contract<Publication>()
             .IsNotNull(User, "User");
             
            AddNotifications(contract);
        }

        public void EditInfo(string text, string? imageURL, string? midiaURL, EnableProperty status)
        {
            Text = text;
            ImageURL = imageURL;
            MidiaURL = midiaURL;
            UpdatedOn = DateTime.Now;
            DataValidate();
        }
    }
}

