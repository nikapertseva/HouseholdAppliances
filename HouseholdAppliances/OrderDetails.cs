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
    public class OrderDetails : IDB
    {
        private List<OrderDetailsLine> lineCollection = new List<OrderDetailsLine>();
        
        public void AddItem()
        {
            if (lineCollection.Count==0)
            {
                lineCollection.Add(new OrderDetailsLine
                {
                    OrderDetailsId = 1,
                    OrderId = 0,
                    OrderGoods = new GoodsLine(),
                    OrderQuantity = 0
                    
                });
            }
            else 
            {
                lineCollection.Add(new OrderDetailsLine
                {
                    OrderDetailsId = lineCollection.Last().OrderDetailsId + 1,
                    OrderId = 0,
                    OrderGoods = new GoodsLine(),
                    OrderQuantity = 0
                    
                });
            }
                       
        }
        
        public void RemoveLine(OrderDetailsLine orderDetails)
        {
            lineCollection.RemoveAll(l => l.OrderDetailsId == orderDetails.OrderDetailsId);
        }

        public IEnumerable<OrderDetailsLine> Lines
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            get { return lineCollection; }
        }

        public void Serialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<OrderDetailsLine>));

            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, lineCollection);
            }
        }
        public void Deserialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<OrderDetailsLine>));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                List<OrderDetailsLine> newOrderDetails = (List<OrderDetailsLine>)jsonFormatter.ReadObject(fs);

                foreach (OrderDetailsLine o in newOrderDetails)
                {
                    lineCollection.Add(o);
                }
            }
        }
    }
    public class OrderDetailsLine
    {
        public int OrderDetailsId;
        public int OrderId;
        public GoodsLine OrderGoods;
        public int OrderQuantity;
       
    }
}
