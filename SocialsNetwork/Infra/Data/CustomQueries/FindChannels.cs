using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Streams;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindChannels
    {
        private readonly IConfiguration Configuration;
        public FindChannels(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<ChannelsResponse> Execute(string publicationId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"";
            return data.Query<ChannelsResponse>(query, new { publicationId, page, rows });
        }
    }
}