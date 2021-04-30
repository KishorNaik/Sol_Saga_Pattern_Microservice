using AutoMapper;
using Framework.Saga.Contexts;
using Framework.Saga.Cores;
using MediatR;
using SalesOrder.Commnad.Api.Applications.Saga.Data;
using SalesOrder.Commnad.Api.Applications.Saga.RollBack;
using SalesOrder.Commnad.Api.Applications.Saga.Start;
using SalesOrder.Shared.DTO;
using Stock.Shared.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SalesOrder.Commnad.Api.Applications.Saga.Orchestration
{
    public class SalesOrderOrchestration : IRequest<bool>
    {
        public ISalesOrderDTO SalesOrder { get; set; }
    }

    public sealed class SalesOrderOrchestrationHandler : IRequestHandler<SalesOrderOrchestration, bool>
    {
        private readonly ISagaInvoker sagaInvoker = null;
        private readonly IMediator mediator = null;
        private readonly IMapper mapper = null;

        public SalesOrderOrchestrationHandler(ISagaInvoker sagaInvoker, IMediator mediator, IMapper mapper)
        {
            this.sagaInvoker = sagaInvoker;
            this.mediator = mediator;
            this.mapper = mapper;
        }

        async Task<bool> IRequestHandler<SalesOrderOrchestration, bool>.Handle(SalesOrderOrchestration request, CancellationToken cancellationToken)
        {
            try
            {
                var sagaContext =
                    await
                    sagaInvoker
                    .Add<SagaData>(
                        sagaStartHandler: new StockUpdateSagaStart(mediator, mapper),
                        rule: (sagaContext, transaction) =>
                        {
                            if (sagaContext.Data.Stock.Status == "Quantity available")
                            {
                                transaction.Transaction = Framework.Saga.Contexts.Transaction.Commit;
                            }
                            else
                            {
                                transaction.Transaction = Framework.Saga.Contexts.Transaction.RollBackOrCompensate;
                            }
                        },
                        rollBackOrCompensateContexts: new List<RollBackOrCompensateContext<SagaData>>()
                        {
                            new RollBackOrCompensateContext<SagaData>()
                            {
                                RollBackOrder=1,
                                rollBackOrCompansate=new CancelSalesOrderSagaRollBack(mediator)
                            }
                        }
                        )
                    .Add<SagaData>(
                        sagaStartHandler: new CreateOrderDispatchSagaStart(mediator),
                        rule: (sagaContext, transaction) =>
                        {
                            if (sagaContext.Data.OrderDispatch.Status == "True")
                            {
                                transaction.Transaction = Transaction.Commit;
                            }
                            else
                            {
                                transaction.Transaction = Transaction.RollBackOrCompensate;
                            }
                        },
                        rollBackOrCompensateContexts: new List<RollBackOrCompensateContext<SagaData>>()
                        {
                            new RollBackOrCompensateContext<SagaData>()
                            {
                                RollBackOrder=2,
                                rollBackOrCompansate=new CancelSalesOrderSagaRollBack(mediator)
                            },
                            new RollBackOrCompensateContext<SagaData>()
                            {
                                RollBackOrder=1,
                                rollBackOrCompansate=new StockReverseSagaRollBack(mediator)
                            }
                        }
                        )
                    .Run<SagaData>(new SagaContext<SagaData>()
                    {
                        Data = new SagaData()
                        {
                            SalesOrder = request.SalesOrder
                        },
                        Error = default(Exception)
                    });

                return true;
            }
            catch
            {
                throw;
            }
        }
    }
}