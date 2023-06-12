using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Socials;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindPublicationComments
    {
        private readonly IConfiguration Configuration;
        public FindPublicationComments(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<CommentResponse> Execute(string publicationId, int page, int rows)
        {
          
            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
            SELECT 
            aspUsers.Id AS 'User',
            aspUsers.Email,
            aspUsers.AvatarURL,
            CMM.Id as 'Comment',
            CMM.CreatedOn,
            CMM.CommentValue as 'Text',
            CMM.ImageURL as 'ImageURL',
            CMM.MidiaURL 'MidiaURL',
            CMM.PublicationId as 'Publication',
            CMM.UpdatedOn
            FROM Comments AS CMM
            INNER JOIN AspNetUsers AS aspUsers ON (aspUsers.Id = CMM.UserId)
            INNER JOIN AspNetUserClaims AS aspClaims ON (aspUsers.Id = aspClaims.UserId AND aspClaims.ClaimType = 'name')
            WHERE
            CMM.PublicationId = @publicationId
            ORDER BY CMM.CreatedOn DESC
            OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<CommentResponse>(query, new { publicationId , page, rows });
        }
    }
}
