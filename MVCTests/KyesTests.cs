using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MVC.Crypto;

namespace MVCTests
{
    /// <summary>
    /// Summary description for KyesTests
    /// </summary>
    [TestClass]
    public class KyesTests
    {
        [TestClass]
        public partial class KeysTest
        {
            [TestMethod]
            [ExpectedException(typeof(ArgumentOutOfRangeException))]
            public void GetRandomKeyTestNegativeInput()
            {
                Keys.GetRandomKey(-10);
            } 


            [TestMethod]
            public void GetRandomKeyTest()
            {
                for (int i = 0; i < 20; i++)
                {
                    var key = Keys.GetRandomKey(i);
                    Assert.AreEqual(key.Length, i, $"result of Keys.GetRandomKey({i}) should have a length of {i} but key {key} has a length of {key.Length}");
                }
            }

            [TestMethod]
            public void GetRandomKeyTestRandom()
            {
                for (int i = 5; i < 50; i++)
                {
                    var key1 = Keys.GetRandomKey(i);
                    var key2 = Keys.GetRandomKey(i);

                    Assert.AreNotEqual(key1, key2);
                }
            }
        }
    }
}
