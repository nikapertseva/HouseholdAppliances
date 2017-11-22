using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HouseholdAppliances
{
    interface IDB
    {
        void AddItem();

        void Serialize(string Path);
        void Deserialize(string Path);
    }
}
