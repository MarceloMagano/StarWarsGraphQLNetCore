using StarWarsGraphQLNetCore.Core.Models;
using System.Threading.Tasks;

namespace StarWarsGraphQLNetCore.Core.Data
{
    public interface IDroidRepository
    {
        Task<Droid> Get(int id);
    }
}
