using KafkaMessaging;
using Messages;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SignalRHub.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SignalRHub
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
            services.Configure<SHubConfig>(Configuration);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
            {
                builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowAnyOrigin()
                .AllowCredentials();
            }));

            services.AddSignalR();
            services.AddSingleton<IMessageClient, KafkaClient>();
            services.AddSingleton(typeof(HubMessageClient));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("CorsPolicy");
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotifyHub>("/notify");
            });

            ConfigureKafka(app);

            app.UseMvc();
        }

        private void ConfigureKafka(IApplicationBuilder app)
        {
            HubMessageClient hubKafkaClient = (HubMessageClient)app.ApplicationServices.GetService<HubMessageClient>();

            SHubConfigurator sHubConfigurator = new SHubConfigurator();
            hubKafkaClient.Settings = sHubConfigurator.UpdateConfigIfRanInContainer(hubKafkaClient.Settings);
            hubKafkaClient.Connect();
            hubKafkaClient.StartListening();
        }
    }
}
