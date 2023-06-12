using Flunt.Notifications;
using Flunt.Validations;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Models.Socials
{
    public class CommentReaction : Notifiable<Notification>
    {
        public CommentReaction() { }
        public Guid Id { get; set; }
        public Comment Comment { get; set; }
        public SubComment? subComment { get; set; }
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }
        public int ReactTypeFK { get; set; }
        public TypeReaction ReactType { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public CommentReaction(Comment comment, SubComment? subComment, ApplicationUser user, TypeReaction react)
        {

            var contract = new Contract<CommentReaction>()
                .IsNotNull(comment,"Comment")
                .IsNotNull(subComment,"SubComment")
                .IsNotNull(user,"User")
                .IsNotNull(react,"React");
            AddNotifications(contract);

            Id = Guid.NewGuid();
            Comment = comment;
            this.subComment = subComment;
            User = user;
            ReactType = react;
            CreatedOn = DateTime.Now;
           
        }
    }
}
