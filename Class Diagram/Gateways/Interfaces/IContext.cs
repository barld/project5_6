using DataModels.Gateways;

namespace DataModels
{
    public interface IContext
    {   
        UserGateway Users { get; }
        IGameGateway Games { get; }
        PlatformGateway Platforms { get; }
    }
}