using Flunt.Notifications;
using Flunt.Validations;

namespace SocialsNetwork.Models.Class
{
    public class Follow : Notifiable<Notification>
    {
        public Follow() { }

        public Guid Id { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationUser FollowedUser { get; set; }
        public DateTime CreatedOn { get; set; }

        public Follow(ApplicationUser user, ApplicationUser followedUser)
        {
            var contract = new Contract<Follow>()
                .IsNotNull(user, "User")
                .IsNotNull(followedUser, "FollowedUser");
            AddNotifications(contract);

            Id = new Guid();
            User = user;
            FollowedUser = followedUser;
            CreatedOn = DateTime.Now;
        }
    }
}
