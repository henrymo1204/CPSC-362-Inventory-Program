using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace login.classes
{
    public class OrderDetail
    {
        public string OrderID { get; set; }

        public string ProductName { get; set; }

        public int Quantity { get; set; }

        public int Cost { get; set; }
    }
}
