using Flunt.Notifications;
using Flunt.Validations;

namespace SocialsNetwork.Models.StreamSpace
{
    public class ChannelCategories : Notifiable<Notification>
    {
        public ChannelCategories() { }

        public Guid Id { get; set; }
        public StreamChannel IdChannel { get; set; }
        public DateTime CreatedOn { get; set; }
        public StreamingCategories Streaming { get; set; }

        public ChannelCategories(StreamChannel Channel, StreamingCategories Categories)
        {
            var contract = new Contract<ChannelCategories>()
                .IsNull(IdChannel, "Channel")
                .IsNull(Streaming, "Categories");
            AddNotifications(contract);

            Id = Guid.NewGuid();
            IdChannel = Channel;
            Streaming = Categories;
            CreatedOn = DateTime.Now;
        }
    }
}
