using Framework.Saga.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Contexts
{
    public class SagaInvokerContext<TData> where TData : class, ISagaData
    {
        public ISagaStart<TData> SagaStart { get; set; }

        public Action<SagaContext<TData>, SagaTransaction> Rule { get; set; }

        public IEnumerable<RollBackOrCompensateContext<TData>> RollBackOrCompensates { get; set; }
    }
}