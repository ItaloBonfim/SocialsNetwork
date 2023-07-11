using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SocialsNetwork.Infra.Data;
using SocialsNetwork.Models.Class;
using System.Security.Claims;
using SocialsNetwork.Models.Class.Enums;
using SocialsNetwork.Models.Socials;
using SocialsNetwork.DTO.Class.Specialist;

namespace SocialsNetwork.BackConfigurations
{//https://www.youtube.com/watch?v=0B7QjRSvcGI
    public static class Initializer
    {
        public static async Task<WebApplication> SeedUsuarios(this WebApplication app)
        {
            #region PUPULA A BASE COM USUARIOS PRONTOS PARA TESTES
            using (var scope = app.Services.CreateScope())
            {
                using var userManager =  scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                try
                {
                    
                    var register = await userManager.Users.FirstOrDefaultAsync();
                   
                    if (register == null)
                    {
                        foreach (var item in usersList())
                        {
                            var data = new ApplicationUser(item.BirthDate, item.Genre)
                            {
                                UserName = item.Email,
                                Email = item.Email,
                                PhoneNumber = item.PhoneNumber,
                                EmailConfirmed = true
                            };

                            var result =  userManager.CreateAsync(data, item.Password).Result;
                           
                            var claim =  userManager.AddClaimAsync(data, new Claim(ClaimTypes.NameIdentifier, data.Id)).Result;
                                claim =  userManager.AddClaimAsync(data, new Claim("Name", item.Name)).Result;

                            if (item.Email.ToLower().StartsWith("adm.system"))
                            {
                                claim = userManager.AddClaimAsync(data, new Claim("AdminLevel", item.Email)).Result;
                            }
                            
                        }
                    }
                } catch (Exception) {
                    throw;
                }

                return app;
            }
            #endregion
        }
        public static async Task<WebApplication> SeedTiposReacoes(this WebApplication App)
        {
            #region POPULA A BASE COM DADOS PRÉ CADASTRADOS PARA TIPOS DE REAÇÕES

            using (var scope = App.Services.CreateScope())
            {
                using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                try
                {
                    var register = await context.TypeReactions.FirstOrDefaultAsync();

                    if (register == null)
                    {
                        foreach(var item in typesReactionsList())
                        {
                            await context.TypeReactions.AddAsync(item);
                        }
                        await context.SaveChangesAsync();
                    }

                } catch (Exception)
                {
                    throw;
                }
                return App;
            }
            #endregion
        }

        #region LISTAS DE INCLUSÃO DE DADOS SEED
        private static IList<TypeReaction> typesReactionsList()
        {
            IList<TypeReaction> typeReactions = new List<TypeReaction>()
            {
                new TypeReaction("Gargalhada", DateTime.Now, null),
                new TypeReaction("Triste", DateTime.Now, null),
                new TypeReaction("Interessante", DateTime.Now, null),
                new TypeReaction("Surpreso(a)", DateTime.Now, null),
                new TypeReaction("Amei", DateTime.Now, null),
                new TypeReaction("Odiei", DateTime.Now, null),

            };
            return typeReactions;

        }

        public static IList<UserRequest> usersList()
        {
            var emailProvider = "@socialplayers.com";
            IList<UserRequest> dataList = new List<UserRequest>()
            {
                new UserRequest() {Email = "annos-voldigoad"+emailProvider, Password = "395029037465", Name = "Annos Voldigoad", BirthDate = DateTime.Parse("1005-02-15"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "stella-vermillion"+emailProvider, Password = "395029037465", Name = "Stella Vermillion", BirthDate = DateTime.Parse("2003-04-20"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "asta"+emailProvider, Password = "395029037465", Name = "Asta", BirthDate = DateTime.Parse("2006-12-31"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "ikki_kurogane"+emailProvider, Password = "395029037465", Name = "Ikki Kurogane", BirthDate = DateTime.Parse("2003-07-27"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "yuno-Grinberryall"+emailProvider, Password = "395029037465", Name = "Yuno Grinberryall", BirthDate = DateTime.Parse("2006-08-10"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "noelle_Silva"+emailProvider, Password = "395029037465", Name = "Noelle Silva", BirthDate = DateTime.Parse("2005-02-28"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "uzumaki-naruto"+emailProvider, Password = "395029037465", Name = "Uzumaki Naruto", BirthDate = DateTime.Parse("2000-03-08"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "uzumaki-hinata"+emailProvider, Password = "395029037465", Name = "Uzumaki Hinata", BirthDate = DateTime.Parse("2001-04-02"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "misha_necron"+emailProvider, Password = "395029037465", Name = "Misha Necron", BirthDate = DateTime.Parse("2006-11-16"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "sasha_necron"+emailProvider, Password = "395029037465", Name = "Sasha Necron", BirthDate = DateTime.Parse("2006-05-09"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "shadow"+emailProvider, Password = "395029037465", Name = "Cid Kageno", BirthDate = DateTime.Parse("1990-09-11"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "alpha"+emailProvider, Password = "395029037465", Name = "Alpha", BirthDate = DateTime.Parse("1998-01-24"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "beta"+emailProvider, Password = "395029037465", Name = "Beta", BirthDate = DateTime.Parse("1999-01-18"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "aurora"+emailProvider, Password = "478040699448", Name = "Aurora", BirthDate = DateTime.Parse("2000-02-20"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "rimuru-tempest"+emailProvider, Password = "478040699448", Name = "Rimuru Tempest", BirthDate = DateTime.Parse("1985-06-07"), Genre = Genre.Undefinided, PhoneNumber = ""  },
                new UserRequest() {Email = "veldora_tempest"+emailProvider, Password = "478040699448", Name = "Veldora Tempest", BirthDate = DateTime.Parse("1500-08-18"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "shuna-tempest"+emailProvider, Password = "478040699448", Name = "Shuna Tempest", BirthDate = DateTime.Parse("2000-10-12"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "shion-tempest"+emailProvider, Password = "478040699448", Name = "Shion Tempest", BirthDate = DateTime.Parse("1996-11-05"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "benimaru_tempest"+emailProvider, Password = "478040699448", Name = "Benimaru Tempest", BirthDate = DateTime.Parse("1999-12-15"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "akariwatanabe"+emailProvider, Password = "478040699448", Name = "Akari Watanabe", BirthDate = DateTime.Parse("2005-06-06"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "jiroyakuin"+emailProvider, Password = "478040699448", Name = "Jiro Yakuin", BirthDate = DateTime.Parse("2005-03-27"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "kyoukohori"+emailProvider, Password = "535528869511", Name = "Kyouko Hori", BirthDate = DateTime.Parse("2004-04-01"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "izumi-Myamura"+emailProvider, Password = "535528869511", Name = "Izume Myamura", BirthDate = DateTime.Parse("2003-11-03"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "kyouheimiyamura"+emailProvider, Password = "535528869511", Name = "Kyouhei Miyamura", BirthDate = DateTime.Parse("2010-01-26"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "monkey_luffy"+emailProvider, Password = "535528869511", Name = "Monkey D. Luffy", BirthDate = DateTime.Parse("1999-05-02"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "catnami"+emailProvider, Password = "535528869511", Name = "Gata Ladra Nami", BirthDate = DateTime.Parse("1999-05-31"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "roronoa-zoro"+emailProvider, Password = "535528869511", Name = "Roronoa Zoro", BirthDate = DateTime.Parse("1996-07-22"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "julisalexia"+emailProvider, Password = "535528869511", Name = "Julis Alexia", BirthDate = DateTime.Parse("2004-08-04"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "amagiriayato"+emailProvider, Password = "535528869511", Name = "Amagiri Ayato", BirthDate = DateTime.Parse("2003-09-10"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "chun-li"+emailProvider, Password = "k8xKkPy57#%j", Name = "Chun-Li", BirthDate = DateTime.Parse("1968-03-01"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "ryu"+emailProvider, Password = "k8xKkPy57#%j", Name = "Ryu", BirthDate = DateTime.Parse("1964-07-21"), Genre = Genre.Male, PhoneNumber = ""  },
                new UserRequest() {Email = "cammy"+emailProvider, Password = "k8xKkPy57#%j", Name = "Cammy White", BirthDate = DateTime.Parse("1974-01-06"), Genre = Genre.Female, PhoneNumber = ""  },
                new UserRequest() {Email = "kenmasters"+emailProvider, Password = "k8xKkPy57#%j", Name = "Ken Masters", BirthDate = DateTime.Parse("1965-02-14"), Genre = Genre.Male, PhoneNumber = ""  },

            };
            return dataList;
        }

        #endregion

    }
}
