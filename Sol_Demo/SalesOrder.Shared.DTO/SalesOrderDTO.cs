using SalesOrder.Shared.Domain;
using System;
using System.Runtime.Serialization;

namespace SalesOrder.Shared.DTO
{
    public interface ISalesOrderDTO : ISalesOrderModel { }

    [DataContract]
    public class SalesOrderDTO : ISalesOrderDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? SalesOrderIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public Guid? SalesOrderNumber { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public DateTime? OrderDate { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String ProductName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? OrderQty { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public double? UnitPrice { get; set; }
    }
}