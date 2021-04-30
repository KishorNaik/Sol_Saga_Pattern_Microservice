using System;

namespace OrderDispatch.Shared.Domain
{
    public interface IOrderDispatchModel
    {
        Guid? ShippingIdentity { get; set; }

        Guid? OrderIdentity { get; set; }

        DateTime? ShipDate { get; set; }
    }
}