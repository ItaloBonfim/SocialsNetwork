using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Socials;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindSubComments
    {
        private readonly IConfiguration Configuration;
        public FindSubComments(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<ReactionResponseSubComment> Execute(string pCommentId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
                SELECT DISTINCT
                aspUsers.Id AS 'User',
                aspClaim.ClaimValue AS 'name',
                aspUsers.AvatarURL AS 'AvatarURL',
                CMM.Id AS 'pCommentId',
                CMM.CommentValue AS 'pCommentValue',
                CMM.ImageURL AS 'pCommentImageURL',
                CMM.MidiaURL AS 'pCommentMidiaURL',
                CMM.CreatedOn AS 'pCreatedOn',
                CMM.UpdatedOn AS 'pUpdateOn',
                SUBC.Id AS 'sCommentId',
                SUBC.Text AS 'sCommentValue',
                SUBC.ImageUrl AS 'sImangeURL',
                SUBC.MidiaUrl AS 'sMidiaURL',
                SUBC.CreatedOn AS 'sCreatedOn',
                SUBC.UpdatedOn AS 'sUpdateOn',
                
               (SELECT COUNT(Id) FROM CommentReactions AS CR
                    WHERE CR.SubCommentId = SUBC.Id) AS 'QtdReactions'

                FROM SubComments AS SUBC
                INNER JOIN Comments AS CMM ON (CMM.Id = SUBC.CommentId)
                INNER JOIN AspNetUsers AS aspUsers ON (aspUsers.Id = SUBC.UserId)
                INNER JOIN AspNetUserClaims AS aspClaim ON (aspClaim.UserId = aspUsers.Id AND aspClaim.ClaimType = 'name')
                WHERE
                CMM.Id = @pCommentId
                ORDER BY SUBC.CreatedOn DESC
                OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<ReactionResponseSubComment>(query, new { pCommentId, page, rows });
        }
    }
}
