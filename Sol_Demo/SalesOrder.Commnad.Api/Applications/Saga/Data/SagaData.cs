using Framework.Saga.Cores;
using OrderDispatch.Shared.DTO;
using SalesOrder.Shared.DTO;
using Stock.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Saga.Data
{
    public class SagaData : ISagaData
    {
        public ISalesOrderDTO SalesOrder { get; set; }

        public IStockDTO Stock { get; set; }

        public IOrderDispatchDTO OrderDispatch { get; set; }
    }
}