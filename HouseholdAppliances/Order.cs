using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Xml.Serialization;

namespace HouseholdAppliances
{
    public class Order : IDB
    {
        private List<OrderLine> lineCollection = new List<OrderLine>();

        public void AddItem()
        {
            if (lineCollection.Count == 0)
            {
                lineCollection.Add(new OrderLine
                {
                    OrderId = 1,
                    UserId = 0,
                    products = new List<OrderDetailsLine>(),
                    DateOrder = new DateTime(),
                    OrderStatus = "заказано",
                    ReasonFailure = null,
                    Phone = null,
                    Price = 0

                });
            }
            else
            {
                lineCollection.Add(new OrderLine
                {
                    OrderId = lineCollection.Last().OrderId + 1,
                    UserId = 0,
                    products = new List<OrderDetailsLine>(),
                    DateOrder = new DateTime(),
                    OrderStatus = "заказано",
                    ReasonFailure = null,
                    Phone = null,
                    Price = 0

                });
            }

        }

        public void RemoveLine(OrderLine order)
        {
            lineCollection.RemoveAll(l => l.OrderId == order.OrderId);
        }

        public IEnumerable<OrderLine> Lines
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            get { return lineCollection; }
        }

        public void Serialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<OrderLine>));

            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, lineCollection);
            }
        }
        public void Deserialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<OrderLine>));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                List<OrderLine> newOrder = (List<OrderLine>)jsonFormatter.ReadObject(fs);

                foreach (OrderLine o in newOrder)
                {
                    lineCollection.Add(o);
                }
            }
        }
    }
    public class OrderLine
    {
        public int OrderId;
        public int UserId;
        public List<OrderDetailsLine> products;
        public DateTime DateOrder;
        public string OrderStatus;
        public string ReasonFailure;
        public string Phone;
        public decimal Price;
        
    }
}
