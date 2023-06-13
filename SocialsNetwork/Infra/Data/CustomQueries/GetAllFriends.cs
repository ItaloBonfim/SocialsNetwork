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
                @"SELECT DISTINCT
                FR.Id as FriendshipID,
                aspUsers.Id as UserId,
                FR.AskFriendshipId,
                FR.AskedId,
                aspClaim.ClaimValue as name,
                aspUsers.Email as Email,
                aspUsers.AvatarURL as avatarURL,
                FR.CreatedOn as CreatedOn
                FROM AspNetUsers as aspUsers
                INNER JOIN AspNetUserClaims as aspClaim ON(aspUsers.Id = aspClaim.UserId AND aspClaim.ClaimType = 'name')
                INNER JOIN Friendships as FR ON(aspUsers.Id = FR.AskFriendshipId OR aspUsers.Id = FR.AskedId)
                WHERE
                aspUsers.Id = @userLogged
                ORDER BY FR.CreatedOn
                OFFSET(@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return BaseConnection.Query<Friends>(query, new {userLogged, page, rows});
        }
    }
}
