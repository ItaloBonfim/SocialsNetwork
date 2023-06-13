using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.DTO.Class;
using SocialsNetwork.Models.Class;
using System.Reflection.Emit;
using System.Security.Claims;

namespace SocialsNetwork.Infra.Data
{

    //adicionar usuario
    //adicionar requisições de amizade
    //adicionar amizades já aceitas
    //adicionar usuario a lista de bloqueio
    //adicionar seguidores

    public static class ModelBuilderExtensions
    {
        //"@socialplayers.com"
        
        public static void seed(this ModelBuilder modelBuilder)
        {
          
            /* 
              https://stackoverflow.com/questions/70701686/seed-admin-user-to-database-onmodelcreating
            *** #Exemple to seed database with some values
            --> modelBuilder.Entity<ApplicationUser>().HasData(users);
            --> modelBuilder.Entity<IdentityUserClaim<string>>().HasData(claims);
            --> modelBuilder.Entity<Claim>().HasData(claims);
            *** #Remember to call function seed in dbContext
            */

           
            
        }
    }
}
