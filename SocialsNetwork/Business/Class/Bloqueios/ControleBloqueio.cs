using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Interfaces.Class.Business;
using SocialsNetwork.Interfaces.Class.Repository;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Business.Class.Bloqueios
{
    public class ControleBloqueio : IBlock
    {
        public IBlockData methods = new Data.Class.ControleBloqueio();
        public ControleBloqueio() { }

        public BlockList AdicionarUsuario(AppDbContext Context, FindUserAndReturnAll Manager, string Id, string LoggedUser)
        {
            BlockList Obj = methods.Adicionar(Context, Manager, Id, LoggedUser);
            return Obj;
        }

        public List<UserResponseBlock> ListarUsuariosBloqueados(FindBlockListUsers Query, string LoggedUser, int page, int rows) { return methods.Listar(Query, LoggedUser, page, rows); }
        public void RemoverUsuarioBloqueado(AppDbContext Context, string LoggedUser, Guid Id)
        {
            methods.Remover(Context, LoggedUser, Id);
        }

        public bool VerificarExistencia(AppDbContext Context, string Id, string LoggedUser)
        {
            var dataRegister = (from X in Context.BlockLists
                                where (X.User.Id == LoggedUser && X.Blocked.Id == Id) || (X.Blocked.Id == LoggedUser && X.User.Id == Id)
                                select new { X }).FirstOrDefault();

            if (dataRegister != null) return true;

            return false;

        }
    }
}
