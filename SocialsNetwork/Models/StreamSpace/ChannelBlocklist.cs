using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Models.StreamSpace
{
    public class ChannelBlocklist : Notifiable<Notification>
    {
        public ChannelBlocklist() { }

        public Guid Id { get; set; }
        public StreamChannel IdChannel { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public string Motivation { get; set; }
        public DateTime CreatedOn { get; set; }

        public ChannelBlocklist(StreamChannel idChannel, ApplicationUser userId, string? motivation)
        {
            var contract = new Contract<ChannelBlocklist>()
                .IsNotNull(idChannel, "IdChannel")
                .IsNotNull(userId, "UserId");
            AddNotifications(contract);

            Id = new Guid();
            IdChannel = idChannel;
            User = userId;
            Motivation = motivation;   
        }


    }
}
