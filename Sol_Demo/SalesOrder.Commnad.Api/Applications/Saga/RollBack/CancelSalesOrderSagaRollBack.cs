using Framework.Saga.Contexts;
using Framework.Saga.Cores;
using MediatR;
using SalesOrder.Commnad.Api.Applications.Features;
using SalesOrder.Commnad.Api.Applications.Saga.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Saga.RollBack
{
    public class CancelSalesOrderSagaRollBack : ISagaRollBackOrCompansate<SagaData>
    {
        private readonly IMediator mediator = null;

        public CancelSalesOrderSagaRollBack(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task ISagaRollBackOrCompansate<SagaData>.HandleAsync(SagaContext<SagaData> sagaContext)
        {
            try
            {
                await mediator.Send<bool>(new CancelSalesOrderCommand()
                {
                    SalesOrderIdentity = sagaContext?.Data?.SalesOrder?.SalesOrderIdentity
                });
            }
            catch
            {
                throw;
            }
        }
    }
}