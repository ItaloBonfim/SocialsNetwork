using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Interfaces.Class.Business
{
    public interface IBlock
    {
        List<UserResponseBlock> ListarUsuariosBloqueados(FindBlockListUsers Query, string LoggedUser, int page, int rows);
        BlockList AdicionarUsuario(AppDbContext Context, FindUserAndReturnAll Manager, string Id, string LoggedUser);
        void RemoverUsuarioBloqueado(AppDbContext Context, string LoggedUser, Guid Id);
        Boolean VerificarExistencia(AppDbContext Context, string Id, string LoggedUser);
    }
}
