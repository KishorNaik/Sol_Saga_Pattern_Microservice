using Framework.Saga.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Cores
{
    public interface ISagaStart<TData> where TData : class, ISagaData
    {
        Task<SagaContext<TData>> HandleAsync(SagaContext<TData> sagaContext);
    }
}