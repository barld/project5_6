using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Gateways
{
    public interface IPlatformGateWay : IGateway<Platform>
    {
        Task<IEnumerable<Platform>> GetAllByPlatform(Platform platform);
        Task<IEnumerable<Platform>> GetAllByTitle(string searchTitle);
        Task<Platform> GeyByName(string name);
        Task Update(Platform platform);
        Task<Game> GetByTitle(string searchTitle);
        //Task<IEnumerable<Game>> GetByTitleLike(SearchGameModel search);
    }
}
