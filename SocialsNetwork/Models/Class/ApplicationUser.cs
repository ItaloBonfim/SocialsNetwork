using Microsoft.AspNetCore.Identity;
using SocialsNetwork.Models.Class.Enums;
using SocialsNetwork.Models.Socials;
using SocialsNetwork.Models.StreamSpace;

namespace SocialsNetwork.Models.Class
{
    public class ApplicationUser : IdentityUser
    {
        public DateTime CreatedOn { get; set; }
        public DateTime? LastUpdate { get; set; }
        public DateTime BirthDate { get; set; }
        public string? AvatarURL { get; set; }
        public Genre Genre { get; set; }
        public Reaction Reaction { get; set; }
        public List<BlockList> BlockedBy { get; set; }
        public List<BlockList> Blocked { get; set; }
        public List<Follow> Followers { get; set; }
        public List<Follow> Followed { get; set; }
        public List<Friendships> UserFriend { get; set; }
        public List<Friendships> UserFriendly { get; set; }
        public List<FriendRequest> Requested { get; set; }
        public List<FriendRequest> Requests { get; set; }
        public List<ChannelBlocklist>? BlockedByChannel { get; set; }
        public ApplicationUser(DateTime birthDate, Genre genre)
        {
            CreatedOn = DateTime.Now;
            BirthDate = birthDate;
            Genre = genre;
            AvatarURL = null;
            LastUpdate = null;

        }
    }
}
