using DataModels.Gateways;

namespace DataModels
{
    public interface IContext
    {   
        UserGateway Users { get; }
        GameGateway Games { get; }
    }
}