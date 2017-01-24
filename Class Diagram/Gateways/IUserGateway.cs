using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataModels.Gateways
{
    public interface IUserGateway : IGateway<User>
    {
        Task Delete(string columnToMatch, string valueToMatch);
        Task<User> GetByEmail(string email);
        List<MyLists> GetMyLists(User user);
        Task<User> Login(string email, string password);
        Task<User> Register(string email, string pwd, Gender gender, List<MyLists> lists = null, AccountRole role = AccountRole.User);
        void UpdateMyLists(User currentUser, string titleOfList, Game game);
    }
}