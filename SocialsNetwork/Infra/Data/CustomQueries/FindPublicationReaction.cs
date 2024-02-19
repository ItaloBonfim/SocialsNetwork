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

        public IEnumerable<ReactionPublicationResponse> Execute(Guid PublicationId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
            SELECT 
            USERS.ClaimValue AS 'rUserName',
            RUSER.ID AS 'rUserId',
            PREC.PublicationId,
            PUB.UserId AS 'UserPublisherId',
            PUB.CreatedOn,
            (SELECT ClaimValue FROM AspNetUserClaims WHERE AspNetUserClaims.UserId = PUB.UserId AND AspNetUserClaims.ClaimType = 'Name') AS 'UserPublisherName',
            PREC.Id,
            PREC.TypeReactionFK,
            PREC.UserExpression


            FROM 
            Reaction AS PREC
            INNER JOIN Publication AS PUB ON (PUB.Id = PREC.PublicationId)
            INNER JOIN AspNetUserClaims AS USERS ON (USERS.UserId = PREC.UserId)
            INNER JOIN TypeReactions AS TR ON (PREC.TypeReactionFK = TR.Id)
            INNER JOIN AspNetUsers AS RUSER ON (RUSER.Id = PREC.UserId)
            WHERE
            PUB.Id = @PublicationId
            AND USERS.ClaimType = 'Name'
            ORDER BY PUB.CreatedOn DESC
            OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<ReactionPublicationResponse>(query, new { PublicationId, page, rows });
        }
    }
}
