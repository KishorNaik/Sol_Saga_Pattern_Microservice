using System;

namespace SalesOrder.Shared.Domain
{
    public interface ISalesOrderModel
    {
        Guid? SalesOrderIdentity { get; set; }

        Guid? SalesOrderNumber { get; set; }

        DateTime? OrderDate { get; set; }

        String ProductName { get; set; }

        int? OrderQty { get; set; }

        double? UnitPrice { get; set; }
    }
}