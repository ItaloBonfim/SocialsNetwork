using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Models.Socials
{
    public class Reaction : Notifiable<Notification>
    {
        public Reaction() { }
        public Guid Id { get; set; }
        public Publication Publication { get; set; }
        public int UserExpression { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int TypeReactionFK { get; set; }
        public TypeReaction TypeReact { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public Reaction(Publication publication, ApplicationUser user, TypeReaction typeReact)
        {

            var contract = new Contract<Reaction>()
                .IsNotNull(publication, "Publication")
                .IsNotNull(user, "User")
                .IsNotNull(typeReact, "TypeReact");
            AddNotifications(contract);

            Id = Guid.NewGuid();
            Publication = publication;
            User = user;
            TypeReact = typeReact;
            CreatedOn = DateTime.Now;
        }
    }
}
