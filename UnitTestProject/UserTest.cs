using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseholdAppliances;
using System.Linq;

namespace UnitTestProject
{
    /// <summary>
    /// Сводное описание для UserTest
    /// </summary>
    [TestClass]
    public class UserTest
    {
        public UserTest()
        {
            //
            // TODO: добавьте здесь логику конструктора
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Получает или устанавливает контекст теста, в котором предоставляются
        ///сведения о текущем тестовом запуске и обеспечивается его функциональность.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Дополнительные атрибуты тестирования
        //
        // При написании тестов можно использовать следующие дополнительные атрибуты:
        //
        // ClassInitialize используется для выполнения кода до запуска первого теста в классе
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // ClassCleanup используется для выполнения кода после завершения работы всех тестов в классе
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // TestInitialize используется для выполнения кода перед запуском каждого теста 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // TestCleanup используется для выполнения кода после завершения каждого теста
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [TestMethod]
        public void Can_Add_New_User()
        {



            User user = new User();

            // Действие
            user.AddItem();
            user.AddItem();
            List<UserLine> results = user.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].UserId, 1);
            Assert.AreEqual(results[1].UserId, 2);
        }

        [TestMethod]
        public void Can_Remove_User()
        {

            User user = new User();

            // Действие
            user.AddItem();
            user.AddItem();
            user.AddItem();

            UserLine u = user.Lines.ElementAt(1);
            user.RemoveLine(u);
            List<UserLine> results = user.Lines.ToList();
            // Утверждение
            Assert.AreEqual(user.Lines.Where(c => c == u).Count(), 0);
            Assert.AreEqual(user.Lines.Count(), 2);
            Assert.AreEqual(results[0].UserId, 1);
            Assert.AreEqual(results[1].UserId, 3);

        }
    }
}
