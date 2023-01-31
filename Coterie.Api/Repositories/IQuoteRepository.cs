using System.Collections.Generic;
using System.Threading.Tasks;
using Coterie.Api.Models;

namespace Coterie.Api.Repositories
{
    public interface IQuoteRepository
    {
        IEnumerable<State> GetStates();
        Business GetBusiness(string requestedBusiness);
    }
}
