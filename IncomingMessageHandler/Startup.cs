using IncomingMessageHandler.Config;
using KafkaMessaging;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncomingMessageHandler
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddSingleton<IMessageClient, KafkaClient>();
            services.AddSingleton(typeof(IncomingMessageMessageClient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            ConfigureKafka(app);

            app.UseMvc();
        }

        private void ConfigureKafka(IApplicationBuilder app)
        {
            Console.WriteLine("Entering ConfigureKafka");
            IncomingMessageMessageClient incomingMessageMessageClient = app.ApplicationServices.GetService<IncomingMessageMessageClient>();

            IncomingMessageHandlerConfigurator incomingMessageHandlerConfigurator = new IncomingMessageHandlerConfigurator();

            Console.WriteLine($"before {incomingMessageMessageClient.Settings.Messaging.FileContentChannel}");
            incomingMessageMessageClient.Settings = incomingMessageHandlerConfigurator.UpdateConfigIfRanInContainer(incomingMessageMessageClient.Settings);
            Console.WriteLine($"after {incomingMessageMessageClient.Settings.Messaging.FileContentChannel}");
            incomingMessageMessageClient.Connect();
            incomingMessageMessageClient.StartListening();

            Console.WriteLine("Exiting ConfigureKafka");
        }
    }
}
