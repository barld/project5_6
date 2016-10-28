﻿using MVC.Controller;
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
using MVCTestApplication.Model;
namespace MVC.DevelopmentTest.Controller
{
    class UserController : MVC.Controller.Controller
    {
        public DatabaseConnection db;
        public UserModel model = new UserModel();

        public void dbInit()
        {
            string databaseName = "project__5_6";
            db = new DatabaseConnection("mongodb://localhost:27017", databaseName);
            Console.WriteLine("Connecting to database..");
            db.initialize();
            if (db.isConnected)
            {
                Console.WriteLine($"Database is connected to '{databaseName}'..");
            }
            else
            {
                Console.WriteLine($"Could not connect to database {databaseName}");
            }
        }

        //public object GetAllUsers()
        //{
        //    var userList = model.userAll(this);

        //    foreach (User user in userList)
        //    {

        //    }
        //}

        public class Email
        {
            public string email { get; set; }
            public string response { get; set; }
        }

        public string GetTest()
        {
            return "<h1>hoi daniel</h1>";
        }

        public object PostSearch()
        {
            var data = GetBodyFromJson<Email>();
            var userList = model.userSearch(this, data.email);

            foreach (User user in userList)
            {
                data.response += user.userName + "<br>";
            }
            
            return data.response;
        }

        public object PostRegister()
        {
            var data = GetBodyFromJson<User>();
            model.userRegister(this, data);
            return Json(data);
        }

        public class Login
        {
            public string username { get; set; }
            public string password { get; set; }
            public string response { get; set; }
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

        public string Get()
        {
            return "<h1>Login Page</h1>";
        }
    }
}
