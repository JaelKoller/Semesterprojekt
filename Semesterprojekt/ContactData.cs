using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Semesterprojekt
{
    public class ContactData
    {
        public string ContactStatus { get; set; }
        public string ContactNumber { get; set; }
        public string TypeOfContact { get; set; }
        public Dictionary<string, string> Fields { get; set; } = new Dictionary<string, string>();
    }
}
