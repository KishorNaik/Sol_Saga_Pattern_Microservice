using Framework.Saga.Contexts;
using Framework.Saga.Cores;
using MediatR;
using OrderDispatch.Shared.DTO;
using SalesOrder.Commnad.Api.Applications.Services.OrderDispatch;
using SalesOrder.Commnad.Api.Applications.Saga.Data;
using SalesOrder.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesOrder.Commnad.Api.Applications.Rules;

namespace SalesOrder.Commnad.Api.Applications.Saga.Start
{
    public class CreateOrderDispatchSagaStart : ISagaStart<SagaData>
    {
        private readonly IMediator mediator = null;

        public CreateOrderDispatchSagaStart(IMediator mediator)
        {
            this.mediator = mediator;
        }

        async Task<SagaContext<SagaData>> ISagaStart<SagaData>.HandleAsync(SagaContext<SagaData> sagaContext)
        {
            try
            {
                // Add Ship Date
                var addShipDateRule = new AddShipDateRule()
                {
                    SalesOrder = sagaContext?.Data?.SalesOrder,
                    OrderDispatch = new OrderDispatchDTO()
                };

                await mediator.Publish<AddShipDateRule>(addShipDateRule);

                // Create Order Dispatch Service.
                var result = await mediator.Send<IOrderDispatchDTO>(new CreateOrderDispatchService()
                {
                    SalesOrder = sagaContext.Data.SalesOrder,
                    OrderDispatch = addShipDateRule.OrderDispatch
                });

                // Store Order Dispatch Data into Saga Context
                sagaContext.Data.OrderDispatch = result;
            }
            catch (Exception ex)
            {
                sagaContext.Error = ex;
            }

            return sagaContext;
        }
    }
}