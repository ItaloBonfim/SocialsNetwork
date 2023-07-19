using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Socials;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindPublicationsWithClaims
    {
        private readonly IConfiguration Configuration;
        public FindPublicationsWithClaims(IConfiguration configuration)
        {
                this.Configuration = configuration;
        }

        public IEnumerable<PublicationResponse> Execute(string LoggedUser,int page, int rows)
        {
            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
                SELECT DISTINCT
                aspUsers.Id AS 'User',
                aspClaim.ClaimValue AS 'Name',
                aspUsers.AvatarURL AS 'AvatarURL',
                PUB.Id AS 'Publication',
                PUB.Text AS 'Content',
                PUB.ImageURL AS 'ImageURL' ,
                PUB.MidiaURL AS 'MidiaURL',
                PUB.CreatedOn AS 'CreatedOn',
                PUB.UpdatedOn AS 'UpdateOn'

                FROM Publication AS PUB
                    LEFT JOIN AspNetUsers AS aspUsers ON (aspUsers.Id = PUB.UserId)
                        INNER JOIN AspNetUserClaims AS aspClaim ON (aspUsers.Id = aspClaim.UserId AND aspClaim.ClaimType = 'Name' )
                            INNER JOIN Friendships AS FSP ON ( FSP.AskFriendshipId = aspUsers.Id OR FSP.AskedId = aspUsers.Id )
                                LEFT JOIN Follows AS FLL ON ( aspUsers.Id = FLL.FollowedUserId )

                WHERE
 
                 PUB.UserId = @LoggedUser
                 OR FSP.AskFriendshipId = @LoggedUser OR FSP.AskedId = @LoggedUser
                 OR FLL.UserId = @LoggedUser
                 ORDER BY PUB.CreatedOn DESC
                 OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";

         

            return data.Query<PublicationResponse>(query, new { LoggedUser, page, rows });
        }


        public IEnumerable<PublicationResponse> PerfilPublications(string LoggedUser, int page, int rows)
        {
            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
                SELECT DISTINCT
                aspUsers.Id AS 'User',
                aspClaim.ClaimValue AS 'Name',
                aspUsers.AvatarURL AS 'AvatarURL',
                PUB.Id AS 'Publication',
                PUB.Text AS 'Content',
                PUB.ImageURL AS 'ImageURL' ,
                PUB.MidiaURL AS 'MidiaURL',
                PUB.CreatedOn AS 'CreatedOn',
                PUB.UpdatedOn AS 'UpdateOn',

                (SELECT COUNT(Id)
                    FROM Comments AS C
                    WHERE C.PublicationId = PUB.Id ) AS 'QtdComments',

                (SELECT COUNT(Id)
                    FROM Reaction AS RE
                    WHERE RE.PublicationId = PUB.Id ) AS 'QtdReactions'

                FROM Publication AS PUB
                    INNER JOIN AspNetUsers AS aspUsers ON (aspUsers.Id = PUB.UserId)
                    INNER JOIN AspNetUserClaims AS aspClaim ON (aspUsers.Id = aspClaim.UserId AND aspClaim.ClaimType = 'Name')
                WHERE
                aspUsers.Id = @LoggedUser 
                ORDER BY PUB.CreatedOn DESC
                OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";



            return data.Query<PublicationResponse>(query, new { LoggedUser, page, rows });
        }



    }
}
