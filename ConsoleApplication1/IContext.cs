using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    public interface IContext
    {
        UserGateway Users { get; }
        GameGateway Games { get; }
    }
}