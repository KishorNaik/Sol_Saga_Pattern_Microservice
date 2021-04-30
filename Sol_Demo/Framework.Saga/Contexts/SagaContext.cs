using Framework.Saga.Cores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Contexts
{
    public class SagaContext<TData> where TData : class, ISagaData
    {
        public TData Data { get; set; }

        public Exception Error { get; set; }

        //public static explicit operator SagaContext<dynamic>(SagaContext<TData> v)
        //{
        //    return new SagaContext<dynamic>()
        //    {
        //        Data = v.Data,
        //        Error = v.Error
        //    };
        //}

        //public static explicit operator SagaContext<TData>(SagaContext<dynamic> v)
        //{
        //    return new SagaContext<TData>()
        //    {
        //        Data = v.Data,
        //        Error = v.Error
        //    };
        //}
    }
}