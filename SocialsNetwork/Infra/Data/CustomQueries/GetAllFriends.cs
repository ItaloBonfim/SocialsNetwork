using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Class;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class GetAllFriends
    {
        public readonly IConfiguration configuration;
        public GetAllFriends(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<Friends> Execute(string userLogged, int page, int rows)
        {
            var BaseConnection = new SqlConnection(configuration["ConnectionStrings:SqlServer"]);
            var query =
                @"SELECT Distinct
                FSP.Id AS 'FriendshipId',
                FSP.AskFriendshipId,
                FSP.AskedId,
                Claims.ClaimValue AS 'Name',
                aspUsers.Email,
                aspUsers.AvatarURL,
                FSP.CreatedOn,
                aspUsers.BirthDate AS 'dataAniversario'
                FROM Friendships AS FSP
                LEFT JOIN AspNetUsers AS aspUsers ON ( (FSP.AskFriendshipId = @userLogged AND FSP.AskedId = aspUsers.Id)
                                                     OR (FSP.AskFriendshipId = aspUsers.Id AND FSP.AskedId = @userLogged ))
                INNER JOIN AspNetUserClaims as Claims ON (aspUsers.Id = Claims.UserId)
                WHERE
                Claims.ClaimType = 'Name'
                ORDER BY FSP.CreatedOn
                OFFSET(@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return BaseConnection.Query<Friends>(query, new {userLogged, page, rows});
        }
    }
}
