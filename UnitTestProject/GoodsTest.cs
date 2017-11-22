using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseholdAppliances;
using System.Linq;

namespace UnitTestProject
{
    /// <summary>
    /// Сводное описание для GoodsTest
    /// </summary>
    [TestClass]
    public class GoodsTest
    {
        public GoodsTest()
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
        public void Can_Add_New_Goods()
        {



            Goods goods = new Goods();

            // Действие
            goods.AddItem();
            goods.AddItem();
            List<GoodsLine> results = goods.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].GoodsId, 1);
            Assert.AreEqual(results[1].GoodsId, 2);
        }
        [TestMethod]
        public void Can_View_Goods_In_Category()
        {



            Goods goods = new Goods();

            // Действие
            goods.AddItem();
            goods.Lines.Last().CategoryId = 1;
            goods.AddItem();
            goods.Lines.Last().CategoryId = 2;
            goods.AddItem();
            goods.Lines.Last().CategoryId = 1;
            List<GoodsLine> results1 = goods.Lines.ToList();
            List<GoodsLine> results2 = goods.GoodsInCategory(1);
            List<GoodsLine> results3 = goods.GoodsInCategory(2);
            List<GoodsLine> results4 = goods.GoodsInCategory(3);

            // Утверждение
            Assert.AreEqual(results1.Count(), 3);
            Assert.AreEqual(results2.Count(), 2);
            Assert.AreEqual(results3.Count(), 1);
            Assert.AreEqual(results4.Count(), 0);
            Assert.AreEqual(results2[0].GoodsId, 1);
            Assert.AreEqual(results2[1].GoodsId, 3);
            Assert.AreEqual(results3[0].GoodsId, 2);
        }

        [TestMethod]
        public void Can_View_Search_Goods()
        {



            Goods goods = new Goods();

            // Действие
            goods.AddItem();
            goods.Lines.Last().GoodsName = "Товар1";
            goods.AddItem();
            goods.Lines.Last().GoodsName = "Товар2";
            goods.AddItem();
            goods.Lines.Last().GoodsName = "Товар3";
            List<GoodsLine> results1 = goods.Lines.ToList();
            List<GoodsLine> results2 = goods.SearchGoods("1");
            List<GoodsLine> results3 = goods.SearchGoods("2");
            List<GoodsLine> results4 = goods.SearchGoods("Товар");

            // Утверждение
            Assert.AreEqual(results1.Count(), 3);
            Assert.AreEqual(results2.Count(), 1);
            Assert.AreEqual(results3.Count(), 1);
            Assert.AreEqual(results4.Count(), 3);
            Assert.AreEqual(results2[0].GoodsId, 1);
            Assert.AreEqual(results3[0].GoodsId, 2);
        }

        [TestMethod]
        public void Can_Remove_Goods()
        {

            Goods goods = new Goods();

            // Действие
            goods.AddItem();
            goods.AddItem();
            goods.AddItem();

            GoodsLine g = goods.Lines.ElementAt(1);
            goods.RemoveLine(g);
            List<GoodsLine> results = goods.Lines.ToList();
            // Утверждение
            Assert.AreEqual(goods.Lines.Where(c => c == g).Count(), 0);
            Assert.AreEqual(goods.Lines.Count(), 2);
            Assert.AreEqual(results[0].GoodsId, 1);
            Assert.AreEqual(results[1].GoodsId, 3);

        }
    }
}
