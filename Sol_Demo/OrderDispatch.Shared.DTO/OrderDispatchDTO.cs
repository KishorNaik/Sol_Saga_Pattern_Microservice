using OrderDispatch.Shared.Domain;
using System;
using System.Runtime.Serialization;

namespace OrderDispatch.Shared.DTO
{
    public interface IOrderDispatchDTO : IOrderDispatchModel
    {
        String Status { get; set; }
    }

    [DataContract]
    public class OrderDispatchDTO : IOrderDispatchDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? ShippingIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? OrderIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? ShipDate { get; set; }

        public String Status { get; set; }
    }
}