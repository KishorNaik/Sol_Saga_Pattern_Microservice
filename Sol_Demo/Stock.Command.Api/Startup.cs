using Framework.RabbitMQ.Extension;
using Framework.SqlClient.Extensions;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Stock.Command.Api.Applications.Message;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Stock.Command.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRabbitMQService("rabbitmq://localhost", "guest", "guest",
                addConsumer: (config) =>
                {
                    config.AddConsumer<StockUpdateConsumeMessage>();
                    config.AddConsumer<StockRollBackConsumeMessage>();
                },
                receiveEndPoints: (configRM, busFactory) =>
                {
                    configRM.ReceiveEndpoint("stock-update-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<StockUpdateConsumeMessage>(busFactory);
                    });

                    configRM.ReceiveEndpoint("stock-rollback-queue", (configReceiveEndPoint) =>
                    {
                        configReceiveEndPoint.ConfigureConsumer<StockRollBackConsumeMessage>(busFactory);
                    });
                }
                );

            services.AddControllers();

            services.AddAutoMapper(typeof(Startup));
            services.AddMediatR(typeof(Startup));

            services.AddSqlProvider(Configuration.GetConnectionString("DefaultConnection"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Stock.Command.Api", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stock.Command.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}