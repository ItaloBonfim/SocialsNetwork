using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Interfaces.Class.Repository
{
    public interface IBlockData
    {
        List<UserResponseBlock> Listar(FindBlockListUsers Query, string LoggedUser, int page, int rows);
        BlockList Adicionar(AppDbContext context, FindUserAndReturnAll Manager, string Id, string LoggedUser);
        void Remover(AppDbContext Context, string LoggedUser, Guid Id);
    }
}
