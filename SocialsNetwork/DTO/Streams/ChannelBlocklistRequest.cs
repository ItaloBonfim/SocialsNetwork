namespace SocialsNetwork.DTO.Streams
{
    public class ChannelBlocklistRequest
    {
        public Guid channelId { get; set; }
        public string userBlockId { get; set; }
        public string motivation { get; set; }
    }
}
