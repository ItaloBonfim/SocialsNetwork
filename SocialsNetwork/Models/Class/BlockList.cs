using Flunt.Notifications;
using Flunt.Validations;
using Microsoft.AspNetCore.Identity;

namespace SocialsNetwork.Models.Class
{
    public class BlockList : Notifiable<Notification>
    {
        public BlockList() { }
        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser Blocked { get; set; }
        public DateTime CreatedOn { get; set; }


        public BlockList(ApplicationUser user, ApplicationUser blocked)
        {
            var contract = new Contract<BlockList>()
            .IsNotNull(user, "User")
            .IsNotNull(blocked, "Blocked");
            AddNotifications(contract);
            
            Id = new Guid();
            User = user;
            Blocked = blocked;
            CreatedOn = DateTime.Now;
            
        }

    }
}
