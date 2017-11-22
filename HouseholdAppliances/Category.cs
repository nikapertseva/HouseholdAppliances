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
    public class Category : IDB 
    {
       
        private List<CategoryLine> lineCollection = new List<CategoryLine>();

        public void AddItem()
        {
            if (lineCollection.Count == 0)
            {
                lineCollection.Add(new CategoryLine
                {
                    CategoryId = 1,
                    CategoryName = null

                });
            }
            else
            {
                lineCollection.Add(new CategoryLine
                {
                    CategoryId = lineCollection.Last().CategoryId + 1,
                    CategoryName = null

                });
            }

        }

        public void RemoveLine(CategoryLine category)
        {
            lineCollection.RemoveAll(l => l.CategoryId == category.CategoryId);
        }

        public IEnumerable<CategoryLine> Lines
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            get { return lineCollection; }
        }

        public void Serialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<CategoryLine>));

            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, lineCollection);
            }
        }
        public void Deserialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<CategoryLine>));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                List<CategoryLine> newCategory = (List<CategoryLine>)jsonFormatter.ReadObject(fs);

                foreach (CategoryLine g in newCategory)
                {
                    lineCollection.Add(g);
                }
            }
        }
    }
    public class CategoryLine
    {
        public int CategoryId;
        public string CategoryName;
    }
}
