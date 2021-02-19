using API.Extensions;
using DakarRally.Application;
using DakarRally.BackgroundServices;
using DakarRally.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;
using Quartz.Impl;
using System.Collections.Specialized;
using System.Threading.Tasks;

namespace DakarRally
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
            services.AddControllers();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen();

            services.AddPersistence(Configuration);

            services.AddApplication();

            //services.AddSingleton(provider => GetScheduler());

            services.AddBackgroundServices();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Enable middleware to serve generated Swagger as a JSON endpoint.
                app.UseSwagger();

                // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
                // specifying the Swagger JSON endpoint.
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                });
            }

            app.EnsureDatabaseCreated();

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        //private async Task<IScheduler> GetScheduler()
        //{
        //    var properties = new NameValueCollection
        //    {
        //        { "quartz.scheduler.instanceName", "DakarRallyQuartz" },
        //        { "quartz.scheduler.instanceId", "DakarRallyQuartz" },
        //        { "quartz.jobStore.type", "Quartz.Simpl.RAMJobStore, Quartz" },
        //        { "quartz.jobStore.useProperties", "true"},
        //        { "quartz.threadPool.threadCount", "2" },
        //        { "quartz.threadPool.maxConcurrency", "3" },
        //    };
        //    var schedulerFactory = new StdSchedulerFactory(properties);
        //    var scheduler = await schedulerFactory.GetScheduler();
        //    await scheduler.Start();                                                                                  
        //    return scheduler;
        //}
    }
}
