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
    public class Goods: IDB
    {
        private List<GoodsLine> lineCollection = new List<GoodsLine>();
        
        public void AddItem()
        {
            if (lineCollection.Count==0)
            {
                lineCollection.Add(new GoodsLine
                {
                    GoodsId=1,
                    GoodsName = null,
                    Description = null,
                    CategoryId = 0,
                    GoodsPrice = 0
                    
                });
            }
            else 
            {
                lineCollection.Add(new GoodsLine
                {
                    GoodsId=lineCollection.Last().GoodsId+1,
                    GoodsName = null,
                    Description = null,
                    CategoryId = 0,
                    GoodsPrice = 0
                    
                });
            }
                       
        }
        
        public void RemoveLine(GoodsLine goods)
        {
            lineCollection.RemoveAll(l => l.GoodsId == goods.GoodsId);
        }

        public IEnumerable<GoodsLine> Lines
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            get { return lineCollection; }
        }

        public List<GoodsLine> GoodsInCategory(int categoryId)
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            return Lines.Where(l => l.CategoryId == categoryId).ToList();
        }
        public List<GoodsLine> SearchGoods(string search)
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            return Lines.Where(l => l.GoodsName.ToLower().Contains(search.ToLower())).ToList();
        }
        public void Serialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<GoodsLine>));

            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, lineCollection);
            }
        }
        public void Deserialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<GoodsLine>));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                List<GoodsLine> newGoods = (List<GoodsLine>)jsonFormatter.ReadObject(fs);

                foreach (GoodsLine g in newGoods)
                {
                    lineCollection.Add(g);
                }
            }
        }
    }
    public class GoodsLine
    {
        public int GoodsId;
        public string GoodsName;
        public string Description;
        public int CategoryId;
        public decimal GoodsPrice;

    }
}
