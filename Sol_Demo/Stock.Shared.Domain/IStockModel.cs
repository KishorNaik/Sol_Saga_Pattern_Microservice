using System;

namespace Stock.Shared.Domain
{
    public interface IStockModel
    {
        Guid? StockIdentity { get; set; }

        String ProductName { get; set; }

        int? Quantity { get; set; }
    }
}