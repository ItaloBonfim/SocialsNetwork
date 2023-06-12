using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Class;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindAllUsersWithClaims
    {
        private readonly IConfiguration Configuration;
        public FindAllUsersWithClaims(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<UserResponse> Execute(string userLogged,int page, int rows)
        {
            /*
            * Aqui é utilizado a lib Dapper
            * é necessario configurar uma nova conexao de banco para utilizar
            * O resultado da consulta é convertido para o Response endpoint e o campos devem ser nomeados
            * igualmente
            * é possivel utiliza paginação mais completa nesse metodo
            * é mais performatico na maioria dos casos se comparado ao EF
            */
            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"SELECT
                        aspUsers.Id as Id,
                        aspUsers.UserName as UserName,
                        aspUsers.Email as Email,
                        aspUsers.AvatarURL as avatarURL
                        FROM AspNetUsers as aspUsers
                        INNER JOIN AspNetUserClaims as aspClaim ON (aspUsers.Id = aspClaim.UserId)
                        WHERE 
                        aspUsers.Id <> @userLogged
                        AND aspClaim = 'name'
                        GROUP BY 
                        aspUsers.Id, aspUsers.UserName, 
                        aspUsers.Email, aspUsers.AvatarURL, 
                        aspClaim.ClaimType
                        ORDER BY aspClaim.ClaimType
                        OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<UserResponse>(query, new {userLogged ,page, rows });
        }
    }
}
