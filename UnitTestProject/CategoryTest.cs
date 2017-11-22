using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseholdAppliances;
using System.Linq;

namespace UnitTestProject
{
    /// <summary>
    /// Сводное описание для CategoryTest
    /// </summary>
    [TestClass]
    public class CategoryTest
    {
        public CategoryTest()
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
        public void Can_Add_New_Category()
        {

            
            
            Category category = new Category();

            // Действие
            category.AddItem();
            category.AddItem();
            List<CategoryLine> results = category.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].CategoryId, 1);
            Assert.AreEqual(results[1].CategoryId, 2);
        }
        
        [TestMethod]
        public void Can_Remove_Category()
        {

            Category category = new Category();

            // Действие
            category.AddItem();
            category.AddItem();
            category.AddItem();
            
            CategoryLine cat = category.Lines.ElementAt(1);
            category.RemoveLine(cat);
            List<CategoryLine> results = category.Lines.ToList();
            // Утверждение
            Assert.AreEqual(category.Lines.Where(c => c == cat).Count(), 0);
            Assert.AreEqual(category.Lines.Count(), 2);
            Assert.AreEqual(results[0].CategoryId, 1);
            Assert.AreEqual(results[1].CategoryId, 3);

        }
        
    }
}
