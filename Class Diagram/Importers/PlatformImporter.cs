﻿using DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Diagram.Importers
{
    public interface PlatformImporter
    {
        List<Platform> importPlatforms();
    }
}
