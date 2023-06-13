using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.DTO.Class.Specialist
{
    public class UserRequest
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public DateTime BirthDate { get; set; }
        public Genre Genre { get; set; }
        public string? PhoneNumber { get; set; }


    }

}
