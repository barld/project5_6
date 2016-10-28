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
            controller.db.collectionInsert("user", userdata);
        }

        public List<DataModels.User> userSearch(UserController controller, string email)
        {
            controller.dbInit();
            return controller.db.collectionSearchFor<User>("user", "email", email);
        }

        public List<DataModels.User> userAll(UserController controller)
        {
            controller.dbInit();
            return controller.db.collectionRetrieveAll<User>("user");
        }
    }
}
