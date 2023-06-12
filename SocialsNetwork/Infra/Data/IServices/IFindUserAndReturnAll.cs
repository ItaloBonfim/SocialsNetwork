using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Infra.Data.IServices
{
    public interface IFindUserAndReturnAll
    {
        Task<ApplicationUser> Execute(string userId);
    }
}
