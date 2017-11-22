using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdAppliances
{
    public class Cart
    {
        private List<CartLine> lineCollection = new List<CartLine>();

        // добавление элемента в корзину
        public void AddItem(int id, GoodsLine goods, int quantity)
        {
            CartLine line = lineCollection
                .Where(g => g.Goods.GoodsId == goods.GoodsId)
                .FirstOrDefault();

            if (line == null)
            {
                lineCollection.Add(new CartLine
                {
                    CartId=id,
                    Goods = goods,
                    Quantity = quantity
                });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        // удаление элемента из корзины
        public void RemoveLine(GoodsLine goods)
        {
            lineCollection.RemoveAll(l => l.Goods.GoodsId == goods.GoodsId);
        }


        // вычисление общей стоимости элементов в корзине
        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Goods.GoodsPrice * e.Quantity);

        }

        // очистка корзины путем удаления всех элементов
        public void Clear()
        {
            lineCollection.Clear();
        }


        public IEnumerable<CartLine> Lines
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            get { return lineCollection; }
        }
    }
    public class CartLine
    {
        public int CartId { get; set; }
        public GoodsLine Goods { get; set; }
        public int Quantity { get; set; }
    }
    
}
