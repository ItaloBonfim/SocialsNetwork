using Flunt.Notifications;
using Flunt.Validations;

namespace SocialsNetwork.Models.Class
{
    public class Friendships : Notifiable<Notification>
    {
        public Friendships() { }
        public Guid Id { get; set; }
        public ApplicationUser AskFriendship { get; set; } // usuario quem solicitou
        public ApplicationUser Asked { get; set; } // usuario que foi solicitado
        public DateTime CreatedOn { get; set; }

        public Friendships(ApplicationUser _askFriendship, ApplicationUser _asked)
        {
            var contract = new Contract<Friendships>()
                .IsNotNull(_askFriendship, "AskFriendship")
                .IsNotNull(_asked, "Asked");
            AddNotifications(contract);
            
            Id = new Guid();
            AskFriendship = _askFriendship;
            Asked = _asked;
            CreatedOn = DateTime.Now;
        }
    }
}
