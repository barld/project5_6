using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;

namespace DataModels.Gateways
{
    public interface IGateway<Model>
    {
        Task Delete(string columnToMatch, string valueToMatch);
        Task<IEnumerable<Model>> GetAll();
        Task<Model> GetById(string id);
        Task Insert(Model model);
        Task InsertMany(IEnumerable<Model> collection);
        Task Replace(string searchField, string searchValue, Model model);
    }
}