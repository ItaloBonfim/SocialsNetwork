using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Socials;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindReactionsComment
    {
        private readonly IConfiguration Configuration;
        public FindReactionsComment(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<ReactionResponseComment> Execute(string publicationId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
                        SELECT DISTINCT
                        CMR.Id AS 'Id',
                        CMR.CommentId AS 'CommentFK',
                        CMM.Id AS 'CommentId',
                        CMR.ReactTypeFK AS 'Type',
                        CMR.UserId AS 'User',
                        CMM.PublicationId AS 'Publication',
                        aspUsers.AvatarURL AS 'AvatarURL',
                        aspClaims.ClaimValue AS 'Name'
                        FROM CommentReactions AS CMR
                        INNER JOIN AspNetUsers AS aspUsers ON (aspUsers.Id = CMR.UserId)
                        INNER JOIN AspNetUserClaims AS aspClaims ON (aspUsers.Id = aspClaims.UserId AND aspClaims.ClaimType = 'name')
                        INNER JOIN Comments AS CMM ON (aspUsers.Id = CMM.UserId)
                        INNER JOIN TypeReactions AS TR ON (CMR.ReactTypeFK = TR.Id)
                        WHERE
                        CMM.PublicationId = @publicationId
                        ORDER BY CMM.CreatedOn
                        OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<ReactionResponseComment>(query, new { publicationId, page, rows });
        }
    }
}
