using Framework.Saga.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Contexts
{
    public class RollBackOrCompensateContext<TData> where TData : class, ISagaData
    {
        public int RollBackOrder { get; set; }

        public ISagaRollBackOrCompansate<TData> rollBackOrCompansate { get; set; }
    }
}