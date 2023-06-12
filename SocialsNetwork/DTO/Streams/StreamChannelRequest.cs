using SocialsNetwork.Models.StreamSpace.Enums;
namespace SocialsNetwork.DTO.Streams
{
    public class StreamChannelRequest
    {
        public Guid Id { get; set; }
        public EnableProperty EnablePrivateChannel { get; set; }
        public EnableProperty EnableChannel { get; set; }
        public string ChannelName { get; set; }
        public string? AvatarURL { get; set; }
        public string? ArtURL { get; set; }
        public string UserId { get; set; }
    }
}
