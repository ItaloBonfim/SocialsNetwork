using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Streams;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindChannelBlockList
    {
        private readonly IConfiguration Configuration;
        public FindChannelBlockList(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<ChannelBlocklistResponse> Execute(string channelId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"
            SELECT 
            CHBL.Id AS 'IdBlock',
            CHBL.IdChannelId AS 'IdChannel',
            CHBL.Motivation AS 'Reason',
            CHBL.UserId 'IdUser',
            AspClaim.ClaimValue AS 'Name',
            AspUsers.AvatarURL,
            CHBL.CreatedOn
            FROM ChannelBlocklist AS CHBL
            INNER JOIN AspNetUsers AS AspUsers ON (AspUsers.Id = CHBL.UserId)
            INNER JOIN AspNetUserClaims AS AspClaim ON (AspClaim.UserId = AspUsers.Id)
            WHERE
            CHBL.IdChannelId = '@channelId'
            AND AspClaim.ClaimType = 'Name'
            OFFSET(@page -1) * @rows ROWS FETCH NEXT @rows ROWS ONLY";
            return data.Query<ChannelBlocklistResponse>(query, new { channelId, page, rows });
        }
    }
}