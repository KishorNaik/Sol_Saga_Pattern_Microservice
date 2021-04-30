using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderDispatch.Shared.Message.Responses
{
    public class OrderDispatchResponse
    {
        public Guid? ShippingIdentity { get; set; }

        public Guid? OrderIdentity { get; set; }

        public DateTime? ShipDate { get; set; }

        public String Status { get; set; }
    }
}