using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;
using SocialsNetwork.Models.StreamSpace.Enums;

namespace SocialsNetwork.Models.StreamSpace
{
    public class Subscribes : Notifiable<Notification>
    {
        public Subscribes() { }
        public Guid Id { get; set; }
        public StreamChannel IdChannel { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public EnableProperty IsPrime { get; set; }
        public DateTime EndPrime { get; set; }
        public DateTime CreatedTime { get; set; }

        public Subscribes(string userId, DateTime endPrime, DateTime createdTime)
        {
            var contract = new Contract<Subscribes>()
                .IsNullOrEmpty(userId, "UserId");
            AddNotifications(contract);
            Id = new Guid();
            UserId = userId;
            IsPrime = EnableProperty.Desativado;
            EndPrime = endPrime;
            CreatedTime = DateTime.Now;
        }
    }
}
