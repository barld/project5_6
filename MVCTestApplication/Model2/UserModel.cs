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
using MVCTestApplication;
using MVC.DevelopmentTest.Controller;

namespace MVCTestApplication.Model
{
    class UserModel
    {
        public void userRegister(UserController controller, User userdata)
        {
            controller.dbInit();
            controller.db.collectionInsertUser(userdata);
        }
    }
}
