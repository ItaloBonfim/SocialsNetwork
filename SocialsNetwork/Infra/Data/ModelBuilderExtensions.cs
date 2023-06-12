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
            List<ApplicationUser> users = new List<ApplicationUser>();
            List<Claim> claims = new List<Claim>();

            users.AddRange(dataList());
            claims.AddRange(dataClaims(users));
            
            modelBuilder.Entity<ApplicationUser>().HasData(users);
            // modelBuilder.Entity<IdentityUserClaim<string>>().HasData(claims);
            // modelBuilder.Entity<Claim>().HasData(claims);

            //https://stackoverflow.com/questions/70701686/seed-admin-user-to-database-onmodelcreating
            foreach (var item in users)
            {
                var x = 1;
                modelBuilder.Entity<IdentityUserClaim<string>>().HasData(
                    new IdentityUserClaim<string> { Id = x++ },
                    new IdentityUserClaim<string> { ClaimType = ClaimTypes.NameIdentifier},
                    new IdentityUserClaim<string> { UserId = item.Id },
                    new IdentityUserClaim<string> { ClaimValue = item.UserName }
                    );
            }


        }


        public static List<ApplicationUser> dataList ()
        {
            List<ApplicationUser> list = new List<ApplicationUser>(); 
             
            var passwordHasher = new PasswordHasher<ApplicationUser>();
            foreach (var item in usersList())
            {
                var data = new ApplicationUser(item.BirthDate, item.genre)
                {
                    UserName = item.Name,
                    Email = item.Email,
                    PhoneNumber = item.PhoneNumber,
                 };
                  data.PasswordHash = passwordHasher.HashPassword(data, item.Password);

                  list.Add(data);
            }


            return list;
        } 

        public static List<Claim> dataClaims(List<ApplicationUser> data)
        {

            var userClaims = new List<Claim>();

            foreach(var item  in data)
            {
               userClaims.Add( new Claim(ClaimTypes.NameIdentifier, item.Id));
               userClaims.Add( new Claim("Name", item.UserName));

            }

            return userClaims;
            
        }

        public static IList<UserRequest> usersList()
        {
            var emailProvider = "@socialplayers.com";
            IList<UserRequest> dataList = new List<UserRequest>()
            {
                new UserRequest() {Email = "annos-voldigold"+emailProvider, Password = "395029037465", Name = "Annos Voldigold", BirthDate = DateTime.Parse("1005-02-15"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "stella-vermillion"+emailProvider, Password = "395029037465", Name = "Stella Vermillion", BirthDate = DateTime.Parse("2003-04-20"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "asta"+emailProvider, Password = "395029037465", Name = "Asta", BirthDate = DateTime.Parse("2006-12-31"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "ikki_kurogane"+emailProvider, Password = "395029037465", Name = "Ikki Kurogane", BirthDate = DateTime.Parse("2003-07-27"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "yuno-Grinberryall"+emailProvider, Password = "395029037465", Name = "Yuno Grinberryall", BirthDate = DateTime.Parse("2006-08-10"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "noelle_Silva"+emailProvider, Password = "395029037465", Name = "Noelle Silva", BirthDate = DateTime.Parse("2005-02-28"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "uzumaki-naruto"+emailProvider, Password = "395029037465", Name = "Uzumaki Naruto", BirthDate = DateTime.Parse("2000-03-08"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "uzumaki-hinata"+emailProvider, Password = "395029037465", Name = "Uzumaki Hinata", BirthDate = DateTime.Parse("2001-04-02"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "misha_necron"+emailProvider, Password = "395029037465", Name = "Misha Necron", BirthDate = DateTime.Parse("2006-11-16"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "sasha_necron"+emailProvider, Password = "395029037465", Name = "Sasha Necron", BirthDate = DateTime.Parse("2006-05-09"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "shadow"+emailProvider, Password = "395029037465", Name = "Cid Kageno", BirthDate = DateTime.Parse("1990-09-11"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "alpa"+emailProvider, Password = "395029037465", Name = "Alpa", BirthDate = DateTime.Parse("1998-01-24"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "beta"+emailProvider, Password = "395029037465", Name = "Beta", BirthDate = DateTime.Parse("1999-01-18"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "aurora"+emailProvider, Password = "478040699448", Name = "Aurora", BirthDate = DateTime.Parse("2000-02-20"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "rimuru-tempest"+emailProvider, Password = "478040699448", Name = "Rimuru Tempest", BirthDate = DateTime.Parse("1985-06-07"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "veldora_empest"+emailProvider, Password = "478040699448", Name = "Veldora Tempest", BirthDate = DateTime.Parse("1500-08-18"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "shuna-tempest"+emailProvider, Password = "478040699448", Name = "Shuna Tempest", BirthDate = DateTime.Parse("2000-10-12"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "shion-tempest"+emailProvider, Password = "478040699448", Name = "Shion Tempest", BirthDate = DateTime.Parse("1996-11-05"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "benimaru_tempest"+emailProvider, Password = "478040699448", Name = "Benimaru Tempest", BirthDate = DateTime.Parse("1999-12-15"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "akariwatanabe"+emailProvider, Password = "478040699448", Name = "Akari Watanabe", BirthDate = DateTime.Parse("2005-06-06"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "jiroyakuin"+emailProvider, Password = "478040699448", Name = "Jiro Yakuin", BirthDate = DateTime.Parse("2005-03-27"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "kyoukohori"+emailProvider, Password = "535528869511", Name = "Kyouko Hori", BirthDate = DateTime.Parse("2004-04-01"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "izumi-Myamura"+emailProvider, Password = "535528869511", Name = "Izume Myamura", BirthDate = DateTime.Parse("2003-11-03"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "kyouheimiyamura"+emailProvider, Password = "535528869511", Name = "Kyouhei Miyamura", BirthDate = DateTime.Parse("2010-01-26"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "monkey_luffy"+emailProvider, Password = "535528869511", Name = "Mobky D. Luffy", BirthDate = DateTime.Parse("1999-05-02"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "catnami"+emailProvider, Password = "535528869511", Name = "Gata Ladra Nami", BirthDate = DateTime.Parse("1999-05-31"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "roronoa-zoro"+emailProvider, Password = "535528869511", Name = "Roronoa Zoro", BirthDate = DateTime.Parse("1996-07-22"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "julisalexia"+emailProvider, Password = "535528869511", Name = "Julis Alexia", BirthDate = DateTime.Parse("2004-08-04"), genre = 0, PhoneNumber = ""  },
                new UserRequest() {Email = "amagiriayato"+emailProvider, Password = "535528869511", Name = "Amagiri Ayato", BirthDate = DateTime.Parse("2003-09-10"), genre = 0, PhoneNumber = ""  },
            };
            return dataList;
        }

    }
}
