using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Market_1
{
    public class Product
    {
        public int id { get; set; }
        public String Name { get; set; }
        public int price { get; set; }
        public int Totalprice { get; set; }
        public int AgentID { get; set; }
        public DateTime RegisterDate { get; set; } = DateTime.Now;
    }
}
