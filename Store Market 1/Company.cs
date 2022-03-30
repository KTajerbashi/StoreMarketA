using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Store_Market_1
{
    public class Company
    {
        public int id { get; set; }
        public String CompanyName { get; set; }
        public String CompanyManager { get; set; }
        public Int64 Phone { get; set; }
        public String Email { get; set; }
        public String Address { get; set; }
        public String Site { get; set; }
        public bool Status { get; set; }
    }
}
