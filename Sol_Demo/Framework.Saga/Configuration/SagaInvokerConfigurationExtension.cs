using Framework.Saga.Cores;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Saga.Configuration
{
    public static class SagaInvokerConfigurationExtension
    {
        public static void AddSaga(this IServiceCollection services)
        {
            services.AddTransient<ISagaInvoker, SagaInvoker>();
        }
    }
}