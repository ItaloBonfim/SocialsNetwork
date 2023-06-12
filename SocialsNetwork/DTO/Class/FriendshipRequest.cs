using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.DTO.Class
{
    public class FriendshipRequest
    {
        public Guid Id { get; set; }
        public string AskFriendship { get; set; } // usuario quem solicitou
        public string Asked { get; set; } // usuario que foi solicitado
        public RequestStatus status { get; set; }

        
    }
}
