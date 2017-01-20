using System.Collections.Generic;
using System.Threading.Tasks;
using Class_Diagram;

namespace DataModels.Gateways
{
    public interface IGameGateway : IGateway<Game>
    {
        Task<IEnumerable<Game>> GetAllByPlatform(Platform platform);
        Task<IEnumerable<Game>> GetAllByTitle(string searchTitle);
        Task<Game> GetByEAN(long EAN);
        Task<Game> GetByTitle(string searchTitle);
        Task<IEnumerable<Game>> GetByTitleLike(SearchGameModel search);
    }
}