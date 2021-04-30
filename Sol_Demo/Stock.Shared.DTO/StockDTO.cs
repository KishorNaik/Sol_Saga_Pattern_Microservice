using Stock.Shared.Domain;
using System;
using System.Runtime.Serialization;

namespace Stock.Shared.DTO
{
    public interface IStockDTO : IStockModel
    {
        String Status { get; set; }
    }

    [DataContract]
    public class StockDTO : IStockDTO
    {
        [DataMember(EmitDefaultValue = false)]
        public Guid? StockIdentity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String ProductName { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public int? Quantity { get; set; }

        [DataMember(EmitDefaultValue = false)]
        public String Status { get; set; }
    }
}