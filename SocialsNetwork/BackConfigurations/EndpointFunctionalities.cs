using SocialsNetwork.Endpoints.Class.Blocklists;
using SocialsNetwork.Endpoints.Class.DataTests;
using SocialsNetwork.Endpoints.Class.Follows;
using SocialsNetwork.Endpoints.Class.FriendRequests;
using SocialsNetwork.Endpoints.Class.Friends;
using SocialsNetwork.Endpoints.Class.Users;
using SocialsNetwork.Endpoints.Security;
using SocialsNetwork.Endpoints.Socials.Comments;
using SocialsNetwork.Endpoints.Socials.CommentsReaction;
using SocialsNetwork.Endpoints.Socials.Publications;
using SocialsNetwork.Endpoints.Socials.Reactions;
using SocialsNetwork.Endpoints.Socials.Subcomment;
using SocialsNetwork.Endpoints.Socials.TypeReaction;
using SocialsNetwork.Endpoints.Streams.ChannelBlocks;
using SocialsNetwork.Endpoints.Streams.CHCategories;
using SocialsNetwork.Endpoints.Streams.CHConfigurations;
using SocialsNetwork.Endpoints.Streams.StChannel;
using SocialsNetwork.Endpoints.Streams.StreamingCategories;
using SocialsNetwork.Endpoints.Streams.Subscribes;

namespace SocialsNetwork.BackConfigurations
{
    public class EndpointFunctionalities
    {
        private WebApplication app;

        public EndpointFunctionalities(WebApplication app)
        {
            this.app = app;
        }

        public void ConfigureToken(WebApplication app)
        {
            app.MapMethods(TokenPost.Template, TokenPost.Methods, TokenPost.Handle);
        }
        public void ConfigureDevelopment(WebApplication app)
        {
            app.MapMethods(Tests.Template, Tests.Methods, Tests.Handle);
        }
        public void ConfigureClass(WebApplication app)
        {
            app.MapMethods(UserGet.Template, UserGet.Methods, UserGet.Handle);
            app.MapMethods(UserPost.Template, UserPost.Methods, UserPost.Handle);
            app.MapMethods(UserPut.Template, UserPut.Methods, UserPut.Handle);
            app.MapMethods(BlocklistGet.Template, BlocklistGet.Methods, BlocklistGet.Handle);
            app.MapMethods(BlocklistPost.Template, BlocklistPost.Methods, BlocklistPost.Handle);
            app.MapMethods(BlocklistDelete.Template, BlocklistDelete.Methods, BlocklistDelete.Handle);
            app.MapMethods(FollowsMeGet.Template, FollowsMeGet.Methods, FollowsMeGet.Handle);
            app.MapMethods(FollowsGet.Template, FollowsGet.Methods, FollowsGet.Handle);
            app.MapMethods(FollowsPost.Template, FollowsPost.Methods, FollowsPost.Handle);
            app.MapMethods(Unfollow.Template, Unfollow.Methods, Unfollow.Handle);
            app.MapMethods(ForceUnfollow.Template, ForceUnfollow.Methods, ForceUnfollow.Handle);
            app.MapMethods(FriendRequestGet.Template, FriendRequestGet.Methods, FriendRequestGet.Handle);
            app.MapMethods(FriendshipsGet.Template, FriendshipsGet.Methods, FriendshipsGet.Handle);
            app.MapMethods(FriendRequestPost.Template, FriendRequestPost.Methods, FriendRequestPost.Handle);
            app.MapMethods(FriendRequestCancel.Template, FriendRequestCancel.Methods, FriendRequestCancel.Handle);
            app.MapMethods(FriendshipsDelete.Template, FriendshipsDelete.Methods, FriendshipsDelete.Handle);
            app.MapMethods(NewFriendFriendRequest.Template, NewFriendFriendRequest.Methods, NewFriendFriendRequest.Handle);
            app.MapMethods(ChannelBlocklistPost.Template, ChannelBlocklistPost.Methods, ChannelBlocklistPost.Handle);
        }

        public void ConfigureSocials(WebApplication app)
        {
            app.MapMethods(CommentGet.Template, CommentGet.Methods, CommentGet.Handle);
            app.MapMethods(CommentPost.Template, CommentPost.Methods, CommentPost.Handle);
            app.MapMethods(CommentPut.Template, CommentPut.Methods, CommentPut.Handle);
            app.MapMethods(CommentDelete.Template, CommentDelete.Methods, CommentDelete.Handle);
            app.MapMethods(CommentReactionGet.Template, CommentReactionGet.Methods, CommentReactionGet.Handle);
            app.MapMethods(CommentReactionPost.Template, CommentReactionPost.Methods, CommentReactionPost.Handle);
            app.MapMethods(CommentReactionPut.Template, CommentReactionPut.Methods, CommentReactionPut.Handle);
            app.MapMethods(CommentReactionDelete.Template, CommentReactionDelete.Methods, CommentReactionDelete.Handle);
            app.MapMethods(PublicationGet.Template, PublicationGet.Methods, PublicationGet.Handle);
            app.MapMethods(PublicationPost.Template, PublicationPost.Methods, PublicationPost.Handle);
            app.MapMethods(PublicationPut.Template, PublicationPut.Methods, PublicationPut.Handle);
            app.MapMethods(PublicationDelete.Template, PublicationDelete.Methods, PublicationDelete.Handle);
            app.MapMethods(ReactionGet.Template, ReactionGet.Methods, ReactionGet.Handle);
            app.MapMethods(ReactionPost.Template, ReactionPost.Methods, ReactionPost.Handle);
            app.MapMethods(ReactionPut.Template, ReactionPut.Methods, ReactionPut.Handle);
            app.MapMethods(ReactionDelete.Template, ReactionDelete.Methods, ReactionDelete.Handle);
            app.MapMethods(SubcommentGet.Template, SubcommentGet.Methods, SubcommentGet.Handle);
            app.MapMethods(SubcommentPost.Template, SubcommentPost.Methods, SubcommentPost.Handle);
            app.MapMethods(SubcommentPut.Template, SubcommentPut.Methods, SubcommentPut.Handle);
            app.MapMethods(TypeReactionGet.Template, TypeReactionGet.Methods, TypeReactionGet.Handle);
        }

        public void ConfigureStreams(WebApplication app)
        {
            app.MapMethods(ChannelBlocklistGet.Template, ChannelBlocklistGet.Methods, ChannelBlocklistGet.Handle);
            app.MapMethods(ChannelBlocklistPost.Template, ChannelBlocklistPost.Methods, ChannelBlocklistPost.Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            app.MapMethods(ChannelCategoriesGet.Template, ChannelCategoriesGet.Methods, ChannelCategoriesGet.Handle);
            app.MapMethods(ChannelCategoriesPost.Template, ChannelCategoriesPost.Methods, ChannelCategoriesPost.Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            app.MapMethods(ChannelConfigurationsGet.Template, ChannelConfigurationsGet.Methods, ChannelConfigurationsGet.Handle);
            app.MapMethods(ChannelConfigurationsPost.Template, ChannelConfigurationsPost.Methods, ChannelConfigurationsPost.Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            app.MapMethods(StreamChannelGet.Template, StreamChannelGet.Methods, StreamChannelGet.Handle);
            app.MapMethods(StreamChannelPost.Template, StreamChannelPost.Methods, StreamChannelPost.Handle);
            app.MapMethods(StreamChannelPut.Template, StreamChannelPut.Methods, StreamChannelPut.Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            app.MapMethods(StramingCategoriesGet.Template, StramingCategoriesGet.Methods, StramingCategoriesGet.Handle);
            app.MapMethods(SubscribesGet.Template, SubscribesGet.Methods, SubscribesGet.Handle);
            app.MapMethods(SubscribesPut.Template, SubscribesPut.Methods, SubscribesPut.Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
            //app.MapMethods(.Template, .Methods, .Handle);
          
        }
    }
}
