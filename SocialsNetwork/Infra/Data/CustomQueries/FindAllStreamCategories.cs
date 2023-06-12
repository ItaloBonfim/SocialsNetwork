using Dapper;
using Microsoft.Data.SqlClient;
using SocialsNetwork.DTO.Streams;

namespace SocialsNetwork.Infra.Data.CustomQueries
{
    public class FindAllStreamCategories
    {
        public readonly IConfiguration configuration;
        public FindAllStreamCategories(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IEnumerable<StreamCategoriesResponse> Execute(string userLogged, int page, int rows)
        {
            var BaseConnection = new SqlConnection(configuration["ConnectionStrings:SqlServer"]);
            var query =
                @"";

            return BaseConnection.Query<StreamCategoriesResponse>(query, new { userLogged, page, rows });
        }
    }
}
