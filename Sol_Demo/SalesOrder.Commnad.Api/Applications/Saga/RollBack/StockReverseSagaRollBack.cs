using Framework.Saga.Contexts;
using Framework.Saga.Cores;
using MediatR;
using SalesOrder.Commnad.Api.Applications.Services.Stocks;
using SalesOrder.Commnad.Api.Applications.Saga.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Saga.RollBack
{
    public class StockReverseSagaRollBack : ISagaRollBackOrCompansate<SagaData>
    {
        private readonly IMediator mediator = null;

        public StockReverseSagaRollBack(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task ISagaRollBackOrCompansate<SagaData>.HandleAsync(SagaContext<SagaData> sagaContext)
        {
            try
            {
                _ = await mediator.Send<Unit>(new StockRollBackService()
                {
                    Stock = sagaContext.Data.Stock
                });
            }
            catch
            {
                throw;
            }
        }
    }
}