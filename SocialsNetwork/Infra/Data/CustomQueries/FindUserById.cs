using Microsoft.AspNetCore.Identity;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindUserById
    {
        public UserManager<ApplicationUser> manager { get; private set; }
        public FindUserById(UserManager<ApplicationUser> manager)
        {
            this.manager = manager;
        }

        public async Task<string> Execute(string userID)
        {
            var User = await manager.FindByIdAsync(userID);
            // realizar validação
            
            return User.Id;
            
        }  
    }
}
