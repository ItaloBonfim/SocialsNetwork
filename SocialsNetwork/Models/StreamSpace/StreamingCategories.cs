using Flunt.Notifications;
using Flunt.Validations;

namespace SocialsNetwork.Models.StreamSpace
{
    public class StreamingCategories : Notifiable<Notification>
    {
        public StreamingCategories() { }

        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Abbreviation { get; set; }
        public DateTime CreatedOn { get; set; }
        public List<ChannelCategories> Categorioes;

        public StreamingCategories(string description, string abbreviation)
        {

            var contract = new Contract<StreamingCategories>()
                .IsNullOrEmpty(description, "Description")
                .IsNullOrEmpty(abbreviation, "Abbreviation");
            AddNotifications(contract);

            Id = new Guid();
            Description = description;
            Abbreviation = abbreviation;
            CreatedOn = DateTime.Now;
          
        }
    }
}
