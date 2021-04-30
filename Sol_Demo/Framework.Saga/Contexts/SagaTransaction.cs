using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Contexts
{
    public enum Transaction
    {
        Commit = 0,
        RollBackOrCompensate = 1
    };

    public class SagaTransaction
    {
        public Transaction Transaction { get; set; }
    }
}