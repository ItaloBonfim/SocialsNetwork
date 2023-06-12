using SocialsNetwork.Models.StreamSpace.Enums;
namespace SocialsNetwork.DTO.Streams
{
    public class ChannelConfigurationsRequest
    {
        public Guid Id { get; set; }
        public Guid ChannelId { get; set; }
        public string Description { get; set; }
        public string? alertStartMensage { get; set; }
        public string? StreamLanguage { get; set; }
        public string? BankInfo { get; set; }
        public int? UntilBanishiment { get; set; }
        public double? ValueSubscribe { get; set; }
        public EnableProperty AdultContent { get; set; }
        public EnableProperty AutoDarkTheme { get; set; }
        public EnableProperty ShowOtherStreams { get; set; }
        public EnableProperty EnableSubscribe { get; set; }
        public EnableProperty HidePersonalProfile { get; set; }
    }
}
