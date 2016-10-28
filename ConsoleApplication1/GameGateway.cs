using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Driver;

namespace ConsoleApplication1
{
    public class GameGateway : Gateway<Game>
    {
        public GameGateway(IMongoDatabase connection) : base("game", connection)
        {
        }
    }
}