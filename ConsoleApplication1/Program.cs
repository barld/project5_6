using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModels;
using MongoDB.Driver;
using MongoDB.Bson;
using System.Diagnostics;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            DatabaseConnection dc = new DatabaseConnection("mongodb://localhost:27017", "project__5_6");
            dc.initialize();
            dc.setupSampleData();
            if (dc.isConnected)
            {
                Console.WriteLine("Database is connected");
                if (dc.isGeneratedWithSampleData)
                {
                    Console.WriteLine("Database sample data generated");
                }
            }
            else
            {
                Console.WriteLine("Database connection failed");
            }
            Console.ReadLine();
        }
    }
}
