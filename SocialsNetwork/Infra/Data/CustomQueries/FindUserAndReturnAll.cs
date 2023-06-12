using Microsoft.AspNetCore.Identity;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindUserAndReturnAll
    {
        private UserManager<ApplicationUser> _manager;

        public FindUserAndReturnAll(UserManager<ApplicationUser> manager)
        {
            this._manager = manager;
        }

        public async Task<ApplicationUser> Execute(string userId)
        {
            //var identity = await manager.FindByIdAsync(userId);
            //var teste =  context.ApplicationUsers.Where(x => x.Id == userId);
            // var data = await context.ApplicationUsers.FindAsync(userId);
            var data = await _manager.FindByIdAsync(userId);

            if (data == null) return null;
        
            return data;
        }
    }
}
