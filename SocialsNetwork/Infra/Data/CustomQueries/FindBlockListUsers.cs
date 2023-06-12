using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Class;
using Dapper;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindBlockListUsers
    {

        private readonly IConfiguration Configuration;
        public FindBlockListUsers(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<UserResponseBlock> Execute(string userId, int? page, int? rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
            SELECT
            BLS.Id,
            BLS.UserId,
            Claims.ClaimValue,
            dataUsers.Email,
            dataUsers.AvatarURL,
            BLS.BlockedId
            
            FROM  BlockLists BLS
            INNER JOIN AspNetUsers AS dataUsers ON (BLS.BlockedId = dataUsers.Id)
            INNER JOIN AspNetUserClaims AS Claims ON (dataUsers.Id = Claims.UserId)
            WHERE 
            Claims.ClaimType = 'Name'
            AND BLS.UserId  = @userId
            ORDER BY BLS.CreatedOn
            OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return data.Query<UserResponseBlock>(query, new { userId, page, rows });
        }
    }
}
