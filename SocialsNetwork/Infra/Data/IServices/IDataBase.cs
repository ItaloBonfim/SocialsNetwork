using Microsoft.AspNetCore.Identity;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Infra.Data.IServices
{
    public interface IDataBase
    {

        Task<ApplicationUser> GetDataBase(string user);
        Task<UserManager<IdentityUser>> getDataBase(string user);
        
    }
}
