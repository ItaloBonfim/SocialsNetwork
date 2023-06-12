using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Streams;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindChannelSubscribes
    {
        private readonly IConfiguration Configuration;
        public FindChannelSubscribes(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IEnumerable<ChannelSubscribesResponse> Execute(string publicationId, int page, int rows)
        {

            var data = new SqlConnection(Configuration["ConnectionStrings:SqlServer"]);
            var query = @"";
            return data.Query<ChannelSubscribesResponse>(query, new { publicationId, page, rows });
        }
    }
}