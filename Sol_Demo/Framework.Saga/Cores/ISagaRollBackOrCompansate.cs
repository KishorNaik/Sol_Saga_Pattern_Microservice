using Framework.Saga.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Cores
{
    public interface ISagaRollBackOrCompansate<TData> where TData : class, ISagaData
    {
        Task HandleAsync(SagaContext<TData> sagaContext);
    }
}