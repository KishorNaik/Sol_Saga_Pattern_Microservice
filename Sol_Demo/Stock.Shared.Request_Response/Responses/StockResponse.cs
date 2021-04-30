using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stock.Shared.Message.Responses
{
    public class StockResponse
    {
        public Guid? StockIdentity { get; set; }

        public String ProductName { get; set; }

        public int? Quantity { get; set; }

        public String Status { get; set; }
    }
}