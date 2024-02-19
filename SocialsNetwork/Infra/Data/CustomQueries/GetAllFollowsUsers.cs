using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Class;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class GetAllFollowsUsers
    {
        private readonly IConfiguration configuration;
        public GetAllFollowsUsers(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<FollowResponse> ExecuteFollowing(string userLogged, int? page, int? rows)
        {
            var BaseConnection = new SqlConnection(configuration["ConnectionStrings:SqlServer"]);
            var query = @"
                        SELECT
                        aspUsers.Id as UserId,
                        aspClaim.ClaimValue as Name,
                        aspUsers.Email as Email,
                        aspUsers.AvatarURL as avatarURL,

                        FLL.UserId as 'Follow',

                        FLL.CreatedOn
                        FROM AspNetUsers as aspUsers
                        INNER JOIN AspNetUserClaims as aspClaim ON (aspUsers.Id = aspClaim.UserId AND aspClaim.ClaimType = 'name')
                        INNER JOIN Follows as FLL ON (aspUsers.Id = FLL.FollowedUserId)
                        WHERE
                        FLL.UserId = @userLogged 
                        ORDER BY aspClaim.ClaimValue
                        OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            //realizar validação 
            return BaseConnection.Query<FollowResponse>(query, new { userLogged, page, rows });
        }
        public IEnumerable<FollowResponse> ExecuteFollowers(string userLogged, int? page, int? rows)
        {
            var BaseConnection = new SqlConnection(configuration["ConnectionStrings:SqlServer"]);
            var query = @"
                        SELECT
                        aspUsers.Id as UserId,
                        aspClaim.ClaimValue as Name,
                        aspUsers.Email as Email,
                        aspUsers.AvatarURL as AvatarURL,

                        FLL.FollowedUserId as 'Follow',

                        FLL.CreatedOn
                        FROM AspNetUsers as aspUsers
                        INNER JOIN AspNetUserClaims as aspClaim ON (aspUsers.Id = aspClaim.UserId AND aspClaim.ClaimType = 'name')
                        INNER JOIN Follows as FLL ON (aspUsers.Id = FLL.UserId)
                        WHERE
                        FLL.FollowedUserId = @userLogged
                        ORDER BY aspClaim.ClaimValue
                        OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            //realizar validação 
            return BaseConnection.Query<FollowResponse>(query, new {userLogged ,page, rows });
        }
        public IEnumerable<FollowResponse> FilterFollows(string user, string filter, int? page, int? rows)
        {
            var BaseConnection = new SqlConnection(configuration["ConnectionStrings:SqlServer"]);
            string filterValue = "";
            if(filter != null)
            {
                filterValue = "%" + filter + "%";
            }
            var Query = @"
                        SELECT
                        aspUsers.Id as UserId,
                        aspClaim.ClaimValue as Name,
                        aspUsers.Email as Email,
                        aspUsers.AvatarURL as AvatarURL,

                        FLL.FollowedUserId as Follow,

                        FLL.CreatedOn
                        FROM AspNetUsers as aspUsers
                        INNER JOIN AspNetUserClaims as aspClaim ON (aspUsers.Id = aspClaim.UserId AND aspClaim.ClaimType = 'name')
                        INNER JOIN Follows as FLL ON (aspUsers.Id = FLL.FollowedUserId)
                        WHERE 
                        FLL.UserId = @user
                        AND aspClaim.ClaimValue LIKE @filterValue 
                        
                        ORDER BY aspClaim.ClaimValue
                        OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return BaseConnection.Query<FollowResponse>(Query, new { user, filterValue, page, rows });
        }

    }
}
