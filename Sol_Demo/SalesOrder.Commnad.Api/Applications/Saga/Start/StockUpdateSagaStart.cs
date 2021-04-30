using AutoMapper;
using Framework.Saga.Contexts;
using Framework.Saga.Cores;
using MediatR;
using SalesOrder.Commnad.Api.Applications.Services.Stocks;
using SalesOrder.Commnad.Api.Applications.Saga.Data;
using Stock.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Saga.Start
{
    public class StockUpdateSagaStart : ISagaStart<SagaData>
    {
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public StockUpdateSagaStart(IMediator mediator, IMapper mapper)
        {
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<SagaContext<SagaData>> ISagaStart<SagaData>.HandleAsync(SagaContext<SagaData> sagaContext)
        {
            try
            {
                IStockDTO stockDTO = new StockDTO()
                {
                    ProductName = sagaContext?.Data?.SalesOrder?.ProductName,
                    Quantity = sagaContext?.Data?.SalesOrder?.OrderQty
                };

                //var result = await mediator.Send<IStockDTO>(mapper.Map<StockUpdateService>(stockDTO));

                var result = await mediator.Send<IStockDTO>(new StockUpdateService()
                {
                    Stock = stockDTO
                });

                sagaContext.Data.Stock = result;
            }
            catch (Exception ex)
            {
                sagaContext.Error = ex;
            }

            return sagaContext;
        }
    }
}