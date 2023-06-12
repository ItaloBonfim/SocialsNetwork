using Flunt.Notifications;

namespace SocialsNetwork.Models.Socials
{
    public abstract class SocialsEntity : Notifiable<Notification>
    {
        public SocialsEntity()
        {
            Id = new Guid();
        }
        public Guid Id { get; set; }
        public List<string>? ImageURL { get; set; }
        public List<string>? MidiaURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime EditedOn { get; set; }
    }
}
