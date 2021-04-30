using Framework.Saga.Contexts;
using Framework.Saga.Cores;
using Sol_Demo.Framework.Saga;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga
{
    public class SagaInvoker : ISagaInvoker
    {
        private readonly List<dynamic> sagaInvokerContextList = new List<dynamic>();

        ISagaInvoker ISagaInvoker.Add<TData>(
            ISagaStart<TData> sagaStartHandler,
            Action<SagaContext<TData>, SagaTransaction> rule,
            IEnumerable<RollBackOrCompensateContext<TData>> rollBackOrCompensateContexts)
        {
            SagaInvokerContext<TData> sagaInvokerContext = new SagaInvokerContext<TData>()
            {
                SagaStart = sagaStartHandler,
                Rule = rule,
                RollBackOrCompensates = rollBackOrCompensateContexts
            };
            sagaInvokerContextList.Add(sagaInvokerContext);
            return this;
        }

        async Task<SagaContext<TData>> ISagaInvoker.Run<TData>(SagaContext<TData> sagaContext)
        {
            SagaTransaction sagaTransaction = new SagaTransaction();

            foreach (var sagaInvokeContextX in sagaInvokerContextList)
            {
                var sagaInvokerContext = sagaInvokeContextX as SagaInvokerContext<TData>;

                // Invoke Saga Start Handler.
                sagaContext =
                    await
                    sagaInvokerContext
                    ?.SagaStart
                    ?.HandleAsync(sagaContext);

                InvokeRules(sagaContext, sagaTransaction, sagaInvokerContext);

                bool isBreak = await InvokeRollBackOrCompansate(sagaContext, sagaTransaction, sagaInvokerContext);

                if (isBreak == true)
                {
                    break;
                }
            }

            return sagaContext;

            void InvokeRules<TData>(SagaContext<TData> sagaContext, SagaTransaction sagaTransaction, SagaInvokerContext<TData> sagaInvokerContext) where TData : class, ISagaData
            {
                if (sagaContext.Error == null)
                {
                    // Invoke Bussiness rule for commit or Roll Back
                    sagaInvokerContext
                        ?.Rule
                        ?.Invoke(sagaContext, sagaTransaction);
                }
                else if (sagaContext.Error != null)
                {
                    sagaTransaction.Transaction = Transaction.RollBackOrCompensate;
                }
            }

            async Task<bool> InvokeRollBackOrCompansate<TData>(SagaContext<TData> sagaContext, SagaTransaction sagaTransaction, SagaInvokerContext<TData> sagaInvokerContext) where TData : class, ISagaData
            {
                if (sagaTransaction.Transaction == Transaction.RollBackOrCompensate)
                {
                    var rollBackCompensateContextList = sagaInvokerContext
                     ?.RollBackOrCompensates
                     ?.AsParallel()
                     ?.AsSequential()
                     ?.OrderBy((x) => x.RollBackOrder)
                     ?.ToList();
                    //?.ForEach(async (rollBack) =>
                    //{
                    //    await rollBack.rollBackOrCompansate.HandleAsync(sagaContext);
                    //});

                    var tasks = rollBackCompensateContextList
                        .AsParallel()
                        .AsSequential()
                        .Select((x) => x.rollBackOrCompansate.HandleAsync(sagaContext))
                        .ToList();

                    await Task.WhenAll(tasks);

                    sagaContext.Data = default(TData);

                    return true;
                }
                else
                {
                    sagaContext.Error = null;

                    return false;
                }
            }
        }
    }
}