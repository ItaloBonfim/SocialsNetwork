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
                SELECT
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
                INNER JOIN AspNetUsers AS aspUsers ON (aspUsers.Id = PUB.UserId)
                INNER JOIN AspNetUserClaims AS aspClaim ON (aspUsers.Id = aspClaim.UserId AND aspClaim.ClaimType = 'Name')
                INNER JOIN Friendships AS FSP ON ( (FSP.AskFriendshipId = @LoggedUser AND FSP.AskedId = aspUsers.Id)
                                                     OR (FSP.AskFriendshipId = aspUsers.Id AND FSP.AskedId = @LoggedUser ))
                
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
                PUB.UpdatedOn AS 'UpdateOn'
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
