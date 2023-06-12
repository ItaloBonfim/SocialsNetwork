using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;
using SocialsNetwork.Models.StreamSpace.Enums;

namespace SocialsNetwork.Models.StreamSpace
{
    public class StreamChannel : Notifiable<Notification>
    {
        public StreamChannel() { }
      
        public Guid Id { get; set; }
        public EnableProperty EnablePrivateChannel { get; set; }
        public EnableProperty EnableChannel { get; set; }
        public string ChannelName { get; set; }
        public string? AvatarURL { get; set; }
        public string? ArtURL { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifyOn { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public Guid ConfigurationId { get; set; }
        public ChannelConfiguration Configuration { get; set; }
        public List<ChannelBlocklist> Blocklist { get; set; }
        public List<ChannelCategories> ChannelCategories { get; set; }
        public List<Subscribes> Subscribes { get; set; }

        public StreamChannel(string channelName, string? avatarURL, string? artURL, ApplicationUser userId)
        {
            var contract = new Contract<StreamChannel>()
                .IsNullOrEmpty(channelName, "ChannelName");
                
                
         
            AddNotifications(contract);
            Id = new Guid();
            EnablePrivateChannel = EnableProperty.Ativo;
            EnableChannel = EnableProperty.Desativado;
            ChannelName = channelName;
            AvatarURL = avatarURL;
            ArtURL = artURL;
            CreatedOn = DateTime.Now;
            
        }
    }
}
