using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.Models.Class
{
    public class FriendRequest : Notifiable<Notification>
    {
        public Guid Id { get; set; }
        public virtual ApplicationUser AskFriendship { get; set; } // usuario que solicitou
        public virtual ApplicationUser Asked { get; set; } // usuario que foi solicitado
        public RequestStatus Status { get; set; }
        public DateTime CreatedOn { get; set; }

        public FriendRequest() { }

        public FriendRequest(ApplicationUser _askFriendship, ApplicationUser _asked)
        {
                var contract = new Contract<FriendRequest>()
                    .IsNotNull(_askFriendship, "_askFriendship")
                    .IsNotNull(_asked, "_asked");
                AddNotifications(contract); 

                Id = new Guid();
                AskFriendship = _askFriendship;
                Asked = _asked;
                Status = RequestStatus.awaiting;
                CreatedOn = DateTime.Now;
        }
    }
}
