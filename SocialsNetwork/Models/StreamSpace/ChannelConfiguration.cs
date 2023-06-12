using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.StreamSpace.Enums;

namespace SocialsNetwork.Models.StreamSpace
{
    public class ChannelConfiguration : Notifiable<Notification>
    {
        public ChannelConfiguration() { }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public EnableProperty AdultContent { get; set; }
        public EnableProperty AutoDarkTheme { get; set; }
        public EnableProperty ShowOtherStreams { get; set; }
        public string? alertStartMensage { get; set; }
        public string? StreamLanguage { get; set; }
        public EnableProperty EnableSubscribe { get; set; }
        public string? BankInfo { get; set; }
        public int? UntilBanishiment { get; set; }
        public EnableProperty HidePersonalProfile { get; set; }
        public double? ValueSubscribe { get; set; }
        public int ChannelSettings { get; set; }
        public StreamChannel Channel { get; set; }

        public ChannelConfiguration(
            string? description, EnableProperty? adultContent, EnableProperty? autoDarkTheme,
            EnableProperty? showOtherStreams, string? alertStartMensage, string? streamLanguage,
            EnableProperty? enableSubscribe, string? bankInfo, int? untilBanishiment,
            EnableProperty? hidePersonalProfile, double? valueSubscribe,
            StreamChannel channel)
        {

            var contract = new Contract<ChannelConfiguration>()
                .IsNull(channel, "Channel");
            AddNotifications(contract);

            Id =  new Guid();
            Description = description;
            AdultContent = EnableProperty.Desativado;
            AutoDarkTheme = EnableProperty.Desativado;
            ShowOtherStreams = EnableProperty.Desativado;
            this.alertStartMensage = alertStartMensage;
            StreamLanguage = streamLanguage;
            EnableSubscribe = EnableProperty.Desativado;
            BankInfo = bankInfo;
            UntilBanishiment = untilBanishiment;
            HidePersonalProfile = EnableProperty.Desativado;
            ValueSubscribe = valueSubscribe;
           
        }
    }
}
