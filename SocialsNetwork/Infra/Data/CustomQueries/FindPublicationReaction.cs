using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Socials;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindPublicationReaction
    {
        private readonly IConfiguration Configuration;
        public FindPublicationReaction(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<ReactionPublicationResponse> Execute(string publicationId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
            SELECT
            RCT.Id,
            TRS.Id AS 'ReactionId',
            TRS.Description AS 'Description',
            RCT.PublicationId AS 'Publication',
            aspUsers.Id AS 'User',
            aspUsers.AvatarURL AS 'AvatarURL',
            aspClaim.ClaimValue AS 'Name'
            FROM Reaction AS RCT
            INNER JOIN Publication AS Pub ON (Pub.Id = RCT.PublicationId)
            INNER JOIN TypeReactions AS TRS ON (RCT.TypeReactionFK = TRS.Id)
            INNER JOIN AspNetUsers AS aspUsers ON (aspUsers.Id = RCT.UserId)
            INNER JOIN AspNetUserClaims AS aspClaim ON (aspUsers.Id = aspClaim.userId AND aspClaim.ClaimType = 'Name')
            WHERE
            Pub.Id = @publicationId
            ORDER BY RCT.CreatedOn
            OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<ReactionPublicationResponse>(query, new { publicationId, page, rows });
        }
    }
}
