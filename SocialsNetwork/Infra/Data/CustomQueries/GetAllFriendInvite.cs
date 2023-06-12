using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Class;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class GetAllFriendInvite
    {
        public readonly IConfiguration configuration;
        public GetAllFriendInvite(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<FriendsInviteResponse> Execute(string loggedUser, int? page, int? rows)
        {
            var BaseConnection = new SqlConnection(configuration["ConnectionStrings:SqlServer"]);
            var query = @"SELECT
                FRS.Id AS 'FriendRequestId', 
                FRS.AskFriendshipId AS 'Requester',
                FRS.Status AS 'RequestStatus',
                ASPUSERS.Id AS 'RequesterId',
                ASPUSERS.Email 'RequesterEmail',
                CLAIMS.ClaimValue AS 'Name',
                ASPUSERS.AvatarURL AS 'RequestAvatarURL',
                FRS.CreatedOn
                FROM FriendRequests AS FRS
                INNER JOIN AspNetUsers AS ASPUSERS ON ( ASPUSERS.Id = FRS.AskFriendshipId )
                INNER JOIN AspNetUserClaims AS CLAIMS ON ( ASPUSERS.Id = CLAIMS.UserId )
                WHERE
                FRS.AskedId = @loggedUser
                AND CLAIMS.ClaimType = 'Name'
                ORDER BY FRS.CreatedOn
                OFFSET(@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return BaseConnection.Query<FriendsInviteResponse>(query, new { loggedUser, page, rows });

        }

        public IEnumerable<FriendsInviteResponse> InvitationsSent(string loggedUser, int? page, int? rows)
        {
            var BaseConnection = new SqlConnection(configuration["ConnectionStrings:SqlServer"]);
            var query = @"SELECT
                FRS.Id AS 'FriendRequestId', 
                FRS.AskFriendshipId AS 'Requester',
                FRS.Status AS 'RequestStatus',
                ASPUSERS.Id AS 'RequesterId',
                ASPUSERS.Email 'RequesterEmail',
                CLAIMS.ClaimValue AS 'Name',
                ASPUSERS.AvatarURL AS 'RequestAvatarURL',
                FRS.CreatedOn
                FROM FriendRequests AS FRS
                INNER JOIN AspNetUsers AS ASPUSERS ON ( ASPUSERS.Id = FRS.AskedId )
                INNER JOIN AspNetUserClaims AS CLAIMS ON ( ASPUSERS.Id = CLAIMS.UserId )
                WHERE
                FRS.AskFriendshipId = @loggedUser
                AND CLAIMS.ClaimType = 'Name'
                ORDER BY FRS.CreatedOn
                OFFSET(@page - 1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

            return BaseConnection.Query<FriendsInviteResponse>(query, new { loggedUser, page, rows });
        }
    }
}

