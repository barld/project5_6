using Class_Diagram.Importers.Impl;
using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers
{
    public interface GameImporter
    {
        List<Game> importGames(int desiredAmount);
    }
}
