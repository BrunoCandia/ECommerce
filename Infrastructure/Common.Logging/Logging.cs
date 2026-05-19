using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Common.Logging
{
    public static class Logging
    {
        public static Action<HostBuilderContext, LoggerConfiguration> ConfigureLogger =>
            (context, loggerConfiguration) =>
            {
                var env = context.HostingEnvironment;

                loggerConfiguration
                    .MinimumLevel.Information()
                    .Enrich.FromLogContext()
                    .Enrich.WithProperty("ApplicationName", env.ApplicationName)
                    .Enrich.WithProperty("EnvironmentName", env.EnvironmentName)
                    .Enrich.WithExceptionDetails()
                    .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Warning)
                    .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Warning)
                    .MinimumLevel.Override("System", LogEventLevel.Warning)
                    .WriteTo.Console();

                if (env.IsDevelopment())
                {
                    loggerConfiguration.MinimumLevel.Debug();

                    //loggerConfiguration.MinimumLevel.Override("Catalog", LogEventLevel.Debug);
                    //loggerConfiguration.MinimumLevel.Override("Basket", LogEventLevel.Debug);
                    //loggerConfiguration.MinimumLevel.Override("Discount", LogEventLevel.Debug);
                    //loggerConfiguration.MinimumLevel.Override("Order", LogEventLevel.Debug);
                }

                //Elastic Search
                var elasticUrl = context.Configuration.GetValue<string>("ElasticConfiguration:Uri");

                ////if (!string.IsNullOrWhiteSpace(elasticUrl))
                ////{
                ////    loggerConfiguration.WriteTo.Elasticsearch(new[] { new Uri(elasticUrl) }, opts =>
                ////    {
                ////        opts.MinimumLevel = LogEventLevel.Debug;
                ////        opts.DataStream = new DataStreamName("logs", "eCommerce", "app");
                ////        opts.BootstrapMethod = BootstrapMethod.Failure;
                ////        ////opts.ConfigureChannel = channelOpts =>
                ////        ////{
                ////        ////    channelOpts.BufferOptions = new BufferOptions
                ////        ////    {
                ////        ////    };
                ////        ////};
                ////    });
                ////}

                if (!string.IsNullOrWhiteSpace(elasticUrl))
                {
                    loggerConfiguration.WriteTo.Elasticsearch(
                        new ElasticsearchSinkOptions(new Uri(elasticUrl))
                        {
                            AutoRegisterTemplate = true,
                            AutoRegisterTemplateVersion = AutoRegisterTemplateVersion.ESv8,

                            // Key improvement: per service + per environment index naming
                            IndexFormat =
                                $"ecommerce-{env.ApplicationName?.ToLower().Replace('.', '-')}-" +
                                $"{env.EnvironmentName?.ToLower()}-" +
                                $"{DateTime.UtcNow:yyyy.MM.dd}",

                            // keep debug for dev env, override via config for prod env
                            MinimumLogEventLevel = env.IsDevelopment() ? LogEventLevel.Debug : LogEventLevel.Information

                            //IndexFormat = "ecommerce-Logs-{0:yyyy.MM.dd}",
                            //MinimumLogEventLevel = LogEventLevel.Debug
                        });
                }
            };
    }
}
