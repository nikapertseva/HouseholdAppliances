using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HouseholdAppliances;
using System.Linq;

namespace UnitTestProject
{
    /// <summary>
    /// Сводное описание для CartTest
    /// </summary>
    [TestClass]
    public class CartTest
    {
        public CartTest()
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
        public void Can_Add_New_Lines()
        {
            
            GoodsLine goods1 =new GoodsLine();
            goods1.GoodsId=1;
            goods1.GoodsName="Товар 1";
            GoodsLine goods2 =new GoodsLine();
            goods2.GoodsId = 2;
            goods2.GoodsName = "Товар 2";
            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(1,goods1, 1);
            cart.AddItem(2, goods2, 1);
            List<CartLine> results = cart.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Goods, goods1);
            Assert.AreEqual(results[1].Goods, goods2);
        }
        [TestMethod]
        public void Can_Add_Quantity_For_Existing_Lines()
        {
            GoodsLine goods1 = new GoodsLine();
            goods1.GoodsId = 1;
            goods1.GoodsName = "Товар 1";
            GoodsLine goods2 = new GoodsLine();
            goods2.GoodsId = 2;
            goods2.GoodsName = "Товар 2";
            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(1, goods1, 1);
            cart.AddItem(2, goods2, 1);
            cart.AddItem(3, goods1, 5);
            List<CartLine> results = cart.Lines.ToList();

            // Утверждение
            Assert.AreEqual(results.Count(), 2);
            Assert.AreEqual(results[0].Quantity, 6);    // 6 экземпляров добавлено в корзину
            Assert.AreEqual(results[1].Quantity, 1);
        }
        [TestMethod]
        public void Can_Remove_Line()
        {
            // Организация - создание нескольких тестовых игр
            GoodsLine goods1 = new GoodsLine();
            goods1.GoodsId = 1;
            goods1.GoodsName = "Товар 1";
            GoodsLine goods2 = new GoodsLine();
            goods2.GoodsId = 2;
            goods2.GoodsName = "Товар 2";
            GoodsLine goods3 = new GoodsLine();
            goods3.GoodsId = 3;
            goods3.GoodsName = "Товар 3";

            // Организация - создание корзины
            Cart cart = new Cart();

            // Организация - добавление нескольких игр в корзину
            cart.AddItem(1,goods1, 1);
            cart.AddItem(2,goods2, 4);
            cart.AddItem(3,goods3, 2);
            cart.AddItem(4,goods2, 1);

            // Действие
            cart.RemoveLine(goods2);

            // Утверждение
            Assert.AreEqual(cart.Lines.Where(c => c.Goods == goods2).Count(), 0);
            Assert.AreEqual(cart.Lines.Count(), 2);
        }
        [TestMethod]
        public void Calculate_Cart_Total()
        {
            // Организация - создание нескольких тестовых игр
            GoodsLine goods1 = new GoodsLine();
            goods1.GoodsId = 1;
            goods1.GoodsName = "Товар 1";
            goods1.GoodsPrice = 100;
            GoodsLine goods2 = new GoodsLine();
            goods2.GoodsId = 2;
            goods2.GoodsName = "Товар 2";
            goods2.GoodsPrice = 55;
            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(1, goods1, 1);
            cart.AddItem(2, goods2, 1);
            cart.AddItem(3, goods1, 5);
            decimal result = cart.ComputeTotalValue();

            // Утверждение
            Assert.AreEqual(result, 655);
        }
        [TestMethod]
        public void Can_Clear_Contents()
        {
            GoodsLine goods1 = new GoodsLine();
            goods1.GoodsId = 1;
            goods1.GoodsName = "Товар 1";
            goods1.GoodsPrice = 100;
            GoodsLine goods2 = new GoodsLine();
            goods2.GoodsId = 2;
            goods2.GoodsName = "Товар 2";
            goods2.GoodsPrice = 55;
            // Организация - создание корзины
            Cart cart = new Cart();

            // Действие
            cart.AddItem(1, goods1, 1);
            cart.AddItem(2, goods2, 1);
            cart.AddItem(3, goods1, 5);
            cart.Clear();

            // Утверждение
            Assert.AreEqual(cart.Lines.Count(), 0);
        }
    }
}
