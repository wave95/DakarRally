using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DakarRally.BackgroundServices.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Quartz;

namespace DakarRally.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                })
            .ConfigureServices((hostContext, services) =>
            {
                // Add the required Quartz.NET services
                services.AddQuartz(q =>
                {
                    // Use a Scoped container to create jobs. I'll touch on this later
                    q.UseMicrosoftDependencyInjectionScopedJobFactory();

                    // Create a "key" for the job
                    var jobKey = new JobKey("DakarRallySimulator");

                    // Register the job with the DI container
                    q.AddJob<DakarRallySimulator>(opts => opts.WithIdentity(jobKey));

                    // Create a trigger for the job
                    q.AddTrigger(opts => opts
                        .ForJob(jobKey) // link to the HelloWorldJob
                        .WithIdentity("DakarRallySimulator-trigger") // give the trigger a unique name
                        .WithCronSchedule("0/1 * * * * ?")); // run every 5 seconds
                });

                // Add the Quartz.NET hosted service

                services.AddQuartzHostedService(
                    q => q.WaitForJobsToComplete = true);
            });
    }
}
