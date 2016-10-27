using MVC.Controller;
using DataModels;
using ConsoleApplication1;
using System;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC.View;

namespace MVC.DevelopmentTest.Controller
{
    class UserController : MVC.Controller.Controller
    {
        DatabaseConnection dc;

        public string Get()
        {
            return "<h1>Login Page</h1>";
        }

        public class Login
        {
            public string username { get; set; }
            public string password { get; set; }
            public string response { get; set; }
        }

        public void GetRegister()
        {
            string databaseName = "project__5_6";
            dc = new DatabaseConnection("mongodb://localhost:27017", databaseName);
            Console.WriteLine("Connecting to database..");
            dc.initialize();
            if (dc.isConnected)
            {
                Console.WriteLine($"Database is connected to '{databaseName}'..");
            }
            else
            {
                Console.WriteLine($"Could not connect to database {databaseName}");
            }

            simulateRegisterAccount();

        }

        static List<UserAddress> myAddresses;

        //City, country and postalcode are shared between 'user' and 'address'
        string city;
        string country;
        string postalcode;
        private void simulateRegisterAccount()
        {
            //Ask user for account info to register
            Console.WriteLine("---ACCOUNT INFO---");
            Console.WriteLine("Enter username:");
            string userName = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string password = Console.ReadLine();
            Console.WriteLine("Are you a male? True/False");
            bool isMale = Convert.ToBoolean(Console.ReadLine());
            Console.WriteLine("Enter email:");
            string email = Console.ReadLine();
            Console.WriteLine();
            createAddress();
            Console.WriteLine("Add address? True/False");
            bool addExtraAddress = Convert.ToBoolean(Console.ReadLine());

            //Zolang de user extra addressen wilt toevoegen, roep createAddress() weer aan
            while (addExtraAddress)
            {
                createAddress();
                Console.WriteLine("Add extra address? True/False");
                addExtraAddress = Convert.ToBoolean(Console.ReadLine());
            }

            //Create new user
            User a = new User() { country = country, postalCode = postalcode, userName = userName, password = password, isMale = isMale, email = email, addresses = myAddresses };

            //Add it to the database
            dc.insertUser(a);
        }

        private List<UserAddress> createAddress()
        {
            //Create empty addresses list
            myAddresses = new List<UserAddress>();

            //Ask user for address info
            Console.WriteLine("---ADDRESS INFO---");
            Console.WriteLine("Enter city:");
            city = Console.ReadLine();
            Console.WriteLine("Enter country:");
            country = Console.ReadLine();
            Console.WriteLine("Enter postalcode:");
            postalcode = Console.ReadLine();
            Console.WriteLine("Enter street:");
            string street = Console.ReadLine();
            Console.WriteLine("Enter street number:");
            int streetnr = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Is this you delivery address? True/False");
            bool isDeliveryAddress = Convert.ToBoolean(Console.ReadLine());

            //Add address info to the empty list
            myAddresses.Add(new UserAddress { city = city, country = country, postal_code = postalcode, street = street, snumber = streetnr, isDeliveryAddress = isDeliveryAddress });

            return myAddresses;
        }

        public object PostLogin()
        {
            var data = GetBodyFromJson<Login>();

            if (data.username == "user" && data.password == "secret")
            {
                Cookie cookie = new Cookie("test", "Authenticated");
                data.response = cookie.Value;
            }

            return Json(data.response);
        }
    }
}
