using Framework.Saga.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Cores
{
    public interface ISagaInvoker
    {
        ISagaInvoker Add<TData>(ISagaStart<TData> sagaStartHandler, Action<SagaContext<TData>, SagaTransaction> rule, IEnumerable<RollBackOrCompensateContext<TData>> rollBackOrCompensateContexts = null) where TData : class, ISagaData;

        Task<SagaContext<TData>> Run<TData>(SagaContext<TData> sagaContext) where TData : class, ISagaData;
    }
}