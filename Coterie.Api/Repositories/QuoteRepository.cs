using System.Collections.Generic;
using System.Threading.Tasks;
using Coterie.Api.Models;
using Dapper;
using MySql.Data.MySqlClient;
 
namespace Coterie.Api.Repositories
{
    public class QuoteRepository : IQuoteRepository
    {
        private readonly MySqlConnection _connection;
        public QuoteRepository()
        {
            _connection = new MySqlConnection("Server=localhost;Database=coterie;Uid=root;Pwd=<>");
        }


        public IEnumerable<State> GetStates()
        {
            const string sql = "SELECT * FROM state";

            return _connection.Query<State>(sql);
        }

        public Business GetBusiness(string requestedBusiness)
        {
            const string sql = 
                @"SELECT * FROM business
                  WHERE UPPER(business.ID) = UPPER(@requestedBusiness)";

            return _connection.QueryFirstOrDefault<Business>(sql, new { requestedBusiness });
        }
    }
}
