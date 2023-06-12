using Flunt.Notifications;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Models.Class;
using SocialsNetwork.Models.Socials;
using SocialsNetwork.Models.StreamSpace;

namespace SocialsNetwork.Infra.Data
{
    public class AppDbContext : IdentityDbContext<ApplicationUser> 
    {
        /* Class */
        public DbSet<FriendRequest> FriendRequests { get; set; }
        public DbSet<Friendships> Friendships { get; set; }
        public DbSet<Follow> Follows { get; set; }
        public DbSet<BlockList> BlockLists { get; set; }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }

        /* Socials */
        public DbSet<Publication> Publication { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<SubComment> SubComments { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }
        public DbSet<Reaction> Reaction { get; set; }
        public DbSet<TypeReaction> TypeReactions { get; set; }

        /* StramSpace */
        public DbSet<StreamChannel> StreamChennels { get; set; }
        public DbSet<ChannelBlocklist> ChannelBlocklist { get; set; }
        public DbSet<ChannelCategories> ChannelCategories { get; set; }
        public DbSet<ChannelConfiguration> ChannelConfigurations { get; set; }
        public DbSet<StreamingCategories> StreamingCategories { get; set; }
        public DbSet<Subscribes> Subscribes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           // builder.seed();
            
            builder.Ignore<Notification>();

            builder.Entity<BlockList>()
                .HasOne(e => e.User)
                .WithMany(e => e.BlockedBy);

            builder.Entity<BlockList>()
                .HasOne(e => e.Blocked)
                .WithMany(e => e.Blocked)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Follow>()
                .HasOne(e => e.User)
                .WithMany(e => e.Followers); 

            builder.Entity<Follow>()
                .HasOne(e => e.FollowedUser)
                .WithMany(e => e.Followed)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<FriendRequest>()
                .HasOne(e => e.AskFriendship)
                .WithMany(e => e.Requests);

            builder.Entity<FriendRequest>()
                .HasOne(e => e.Asked)
                .WithMany(e => e.Requested)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<CommentReaction>()
                .HasOne(e => e.Comment)
                .WithMany(e => e.CommentsReactions)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Friendships>()
               .HasOne(e => e.AskFriendship)
               .WithMany(e => e.UserFriend);

            builder.Entity<Friendships>()
              .HasOne(e => e.Asked)
              .WithMany(e => e.UserFriendly)
              .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<TypeReaction>()
                .HasOne(e => e.CommentReaction)
                .WithOne(e => e.ReactType)
                .HasForeignKey<CommentReaction>(e => e.ReactTypeFK);

            builder.Entity<ApplicationUser>()
                .HasOne(e => e.Reaction)
                .WithOne(e => e.User)
                .HasForeignKey<Reaction>(e => e.UserId);

            builder.Entity<TypeReaction>()
                .HasOne(e => e.ReactionUsed)
                .WithOne(e => e.TypeReact)
                .HasForeignKey<Reaction>(e => e.TypeReactionFK);

            builder.Entity<Comment>()
                .HasOne(e => e.Publication)
                .WithMany(e => e.PublicationComments)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<SubComment>()
                .HasOne(e => e.Comment)
                .WithMany(e => e.subComments)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Reaction>()
               .HasOne(e => e.Publication)
               .WithMany(e => e.Reactions)
               .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<ChannelCategories>()
                .HasOne(e => e.IdChannel)
                .WithMany(e => e.ChannelCategories);

            /* builder.Entity<StreamingCategories>()
                 .HasOne(e => e.channelCategories)
                 .WithMany(e => e.IdCategories);  */

            builder.Entity<ChannelCategories>()
                .HasOne(x => x.Streaming)
                .WithMany(x => x.Categorioes);

            builder.Entity<StreamChannel>()
                .HasOne(e => e.Configuration)
                .WithOne(e => e.Channel)
                .HasForeignKey<StreamChannel>(e => e.ConfigurationId);

            builder.Entity<ChannelBlocklist>()
                .HasOne(e => e.IdChannel)
                .WithMany(e => e.Blocklist)
                .OnDelete(DeleteBehavior.ClientCascade);

            builder.Entity<Subscribes>()
                .HasOne(e => e.IdChannel)
                .WithMany(e => e.Subscribes)
                .OnDelete(DeleteBehavior.ClientCascade);
            






        }
    }
}
