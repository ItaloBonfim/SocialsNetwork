using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Streams;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindChannelConfigurations
    {
        private readonly IConfiguration Configuration;
        public FindChannelConfigurations(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<ChannelConfigutionsResponse> Execute(string publicationId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"";
            return data.Query<ChannelConfigutionsResponse>(query, new { publicationId, page, rows });
        }
    }
}