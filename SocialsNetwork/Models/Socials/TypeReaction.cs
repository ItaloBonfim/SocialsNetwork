using Flunt.Notifications;
using Flunt.Validations;

namespace SocialsNetwork.Models.Socials
{
    public class TypeReaction : Notifiable<Notification>
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? EditedOn { get; set; }
        public CommentReaction CommentReaction { get; set; }
        public Reaction ReactionUsed { get; set; }

        public TypeReaction(string description, DateTime createdOn, DateTime? editedOn)
        {
            var contract = new Contract<TypeReaction>()
                .IsNotNull(description, "Description");
            AddNotifications(contract);

            
            Description = description;
            CreatedOn = DateTime.Now;
            EditedOn = editedOn.HasValue ? editedOn : null;

        }
    }
}
