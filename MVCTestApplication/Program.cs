﻿using MVC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MVCTestApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            MVCApp app = new MVCApp("http://127.0.0.1:8080/", Assembly.GetEntryAssembly());
            app.Run();

        }
    }
}
