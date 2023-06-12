using SocialsNetwork.Models.Class.Enums;

namespace SocialsNetwork.DTO.Class
{
    public class UserRequest
    {
       public string Email { get; set; }
       public string Password { get; set; }
       public string Name { get; set; }
       public DateTime BirthDate { get; set; }
       public Genre genre { get; set; }
       public string PhoneNumber { get; set; }
       
      
    }
   
}
