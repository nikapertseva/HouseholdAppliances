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
    public class Promo : IDB
    {
        private List<PromoLine> lineCollection = new List<PromoLine>();

        public void AddItem()
        {
            if (lineCollection.Count == 0)
            {
                lineCollection.Add(new PromoLine
                {
                    PromoId = 1,
                    PromoCode = null,
                    PromoDiscount = 0,
                    GoodsPromo = new List<GoodsLine>()

                });
            }
            else
            {
                lineCollection.Add(new PromoLine
                {
                    PromoId = lineCollection.Last().PromoId + 1,
                    PromoCode = null,
                    PromoDiscount = 0,
                    GoodsPromo = new List<GoodsLine>()

                });
            }

        }

        public void RemoveLine(PromoLine promo)
        {
            lineCollection.RemoveAll(l => l.PromoId == promo.PromoId);
        }

        public IEnumerable<PromoLine> Lines
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            get { return lineCollection; }
        }

        public void Serialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<PromoLine>));

            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, lineCollection);
            }
        }
        public void Deserialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<PromoLine>));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                List<PromoLine> newPromo = (List<PromoLine>)jsonFormatter.ReadObject(fs);

                foreach (PromoLine p in newPromo)
                {
                    lineCollection.Add(p);
                }
            }
        }
    }
    public class PromoLine
    {
        public int PromoId;
        public string PromoCode;
        public decimal PromoDiscount;
        public List<GoodsLine> GoodsPromo;
       
    }
}
