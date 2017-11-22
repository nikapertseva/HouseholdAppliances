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
    public class User : IDB
    {
        private List<UserLine> lineCollection = new List<UserLine>();
        
        public void AddItem()
        {
            if (lineCollection.Count==0)
            {
                lineCollection.Add(new UserLine
                {
                    UserId = 1,
                    LastName = null,
                    FirstName = null,
                    Email = null,
                    Password = null,
                    DateOfBirth = new DateTime(),
                    City = null,
                    Address = null,
                    Phone = null
                    
                });
            }
            else 
            {
                lineCollection.Add(new UserLine
                {
                    UserId=lineCollection.Last().UserId+1,
                    LastName = null,
                    FirstName = null,
                    Email = null,
                    Password = null,
                    DateOfBirth = new DateTime(),
                    City = null,
                    Address = null,
                    Phone = null
                    
                });
            }
                       
        }
        
        public void RemoveLine(UserLine user)
        {
            lineCollection.RemoveAll(l => l.UserId == user.UserId);
        }

        public IEnumerable<UserLine> Lines
        {
            // свойство, которое позволяет обратиться к содержимому корзины
            get { return lineCollection; }
        }

        public void Serialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<UserLine>));

            using (FileStream fs = new FileStream(Path, FileMode.Create))
            {
                jsonFormatter.WriteObject(fs, lineCollection);
            }
        }
        public void Deserialize(string Path)
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(List<UserLine>));
            using (FileStream fs = new FileStream(Path, FileMode.OpenOrCreate))
            {
                List<UserLine> newUser = (List<UserLine>)jsonFormatter.ReadObject(fs);

                foreach (UserLine u in newUser)
                {
                    lineCollection.Add(u);
                }
            }
        }
    }
    public class UserLine
    {
        public int UserId;
        public string LastName;
        public string FirstName;
        public string Email;
        public string Password;
        public DateTime DateOfBirth;
        public string City;
        public string Address;
        public string Phone;
        public bool Access;

        

    }

}
