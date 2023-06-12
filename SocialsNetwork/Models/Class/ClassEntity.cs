using Flunt.Notifications;

namespace SocialsNetwork.Models.Class
{
    public abstract class ClassEntity : Notifiable<Notification>
    {
        public ClassEntity()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public DateTime CreatedOn { get; set; }
        public string CreatedBy { get; set; }
        public DateTime EditedOn { get; set; }
        public string EditedBy { get; set; }

      

    }
}
