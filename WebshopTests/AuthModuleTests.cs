using Microsoft.VisualStudio.TestTools.UnitTesting;
using Webshop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVC;
using WebshopTests.ContextMock;
using DataModels;
using Webshop.Models;

namespace Webshop.Tests
{
    [TestClass()]
    public class AuthModuleTests
    {
        private MockContext context;
        private AuthModule module;

        [TestMethod()]
        public void AuthModuleTest()
        {
            var context = new MockContext();
            var module = new AuthModule(new Session(), context);
        }

        [TestInitialize]
        public void Setup()
        {
            context = new MockContext(true);
            module = new AuthModule(new Session(), context);

            //Create users
            User user1 = context.Users.Register("info@superict.nl", "geheim123", Gender.Male).Result;
            User user2 = context.Users.Register("hallo@barld.nl", "geheim321", Gender.Female, role: AccountRole.Admin).Result;
            User user3 = context.Users.Register("mynameis@jeff.nl", "geheim321", Gender.Unknown).Result;
        }

        [TestMethod()]
        public void LoginTestSucces()
        {
            var result = module.Login(new User { Email = "info@superict.nl", Password = "geheim123" });
            Assert.IsTrue(result.Success);
        }

        [TestMethod()]
        public void LoginTestLoginTwice()
        {
            module.Login(new User { Email = "info@superict.nl", Password = "geheim123" });
            var result = module.Login(new User { Email = "info@superict.nl", Password = "geheim123" });
            Assert.IsFalse(result.Success, "you are not allow to login twice");
        }

        [TestMethod()]
        public void LoginTestWrongPassword()
        {
            var result = module.Login(new User { Email = "info@superict.nl", Password = "geheim123!" });
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void LoginTestNotExistingUser()
        {
            var result = module.Login(new User { Email = "infor@superict.nl", Password = "geheim123" });
            Assert.IsFalse(result.Success);
        }

        [TestMethod()]
        public void RegisterTest()
        {
            var countCurrentUsers = context.Users.GetAll().Result.Count();
            module.Register(new User { Email = "test@example.com", Password = "geheim321", Gender = Gender.Unknown });

            Assert.AreNotEqual(countCurrentUsers, context.Users.GetAll().Result.Count());
        }

        [TestMethod()]
        public void LogoutTest()
        {
            var result = module.Login(new User { Email = "info@superict.nl", Password = "geheim123" });
            Assert.IsTrue(module.LoggedIn);
            module.Logout();
            Assert.IsFalse(module.LoggedIn);
        }

        [TestMethod()]
        public void LoginStatusTestNotLogedIn()
        {
            Assert.IsFalse(module.LoginStatus().IsLogedIn);
        }

        [TestMethod()]
        public void LoginStatusTestLogedIn()
        {
            module.Login(new User { Email = "info@superict.nl", Password = "geheim123" });

            Assert.IsTrue(module.LoginStatus().IsLogedIn);
            Assert.IsInstanceOfType(module.LoginStatus(), typeof(UserLogedInStatus));
            Assert.AreEqual("info@superict.nl", (module.LoginStatus() as UserLogedInStatus).Email);
        }
    }
}