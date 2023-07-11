using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Infra.Data.CustomQueries;
using SocialsNetwork.Interfaces.Class.Repository;
using SocialsNetwork.Models.Class;

namespace SocialsNetwork.Data.Class
{
    public class ControleBloqueio : IBlockData
    {

        public BlockList Adicionar(AppDbContext Context, FindUserAndReturnAll Manager, string Id, string LoggedUser)
        {
           
            ApplicationUser Logged = Manager.Execute(LoggedUser).Result;
            ApplicationUser blocks = Manager.Execute(Id).Result;


            BlockList obj = new (Logged, blocks);

            Context.BlockLists.Add(obj);
            Context.SaveChanges();

            return obj;
        }

        public List<UserResponseBlock> Listar(FindBlockListUsers Query, string LoggedUser, int page, int rows)
        {
            return (List<UserResponseBlock>)Query.Execute(LoggedUser, page, rows);
        }

        public void Remover(AppDbContext Context, string LoggedUser, Guid Id)
        {
            var data = (from BLC in Context.BlockLists
                         join aspUsers in Context.ApplicationUsers on BLC.User.Id equals aspUsers.Id
                         where
                         aspUsers.Id == LoggedUser && BLC.Id == Id
                         select new
                         {
                           BLC
                         }).FirstOrDefault();

            //Criar Validação para caso onde não foi identificado os dados
            if (data == null) return ;

            Context.BlockLists.Remove(data.BLC);
           // Context.SaveChanges();
        }
    }
}
