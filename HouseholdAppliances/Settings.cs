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
    public class Settings
    {
        public string FileCategory;
        public string FileGoods;
        public string FileUser;
        public string FileOrder;
        public string FileOrderDetails;
        public string FilePromo;
        public string AdminLogin;
        public string AdminPassword;
        public Settings()
        {
            FileCategory = "category.json";
            FileGoods = "goods.json";
            FileUser = "user.json";
            FileOrder = "order.json";
            FileOrderDetails = "orderDetails.json";
            FilePromo = "promo.json";
            AdminLogin = "admin";
            AdminPassword = "123";


        }
        public Settings Deserialize()
        {
            DataContractJsonSerializer jsonFormatter = new DataContractJsonSerializer(typeof(Settings));
            using (FileStream fs = new FileStream("settings.json", FileMode.OpenOrCreate))
            {
                Settings newSettings = (Settings)jsonFormatter.ReadObject(fs);

                return newSettings;
                
            }
        }

        
    }
}
