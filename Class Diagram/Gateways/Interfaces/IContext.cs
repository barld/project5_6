using DataModels.Gateways;

namespace DataModels
{
    public interface IContext
    {   
        IUserGateway Users { get; }
        IGameGateway Games { get; }
        PlatformGateway Platforms { get; }
    }
}