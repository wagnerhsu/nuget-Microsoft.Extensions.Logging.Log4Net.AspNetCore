using log4net;
using log4net.Config;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace WebApplication02
{
    public class Program
    {
        private static ILog _logger;
        public static void Main(string[] args)
        {
            InitLog4net();
            _logger.Info("Startup...");
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureLogging(logging =>
                {
                    logging.ClearProviders();
                    logging.SetMinimumLevel(LogLevel.Trace);
                    logging.AddLog4Net();
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void InitLog4net()
        {
            //创建Log4net仓储
            var repository = LogManager.CreateRepository(SunEcisConst.RepositoryName);
            //监控Log4net配置文件
            var log4netConfig = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),
                "log4net.config");
            XmlConfigurator.ConfigureAndWatch(repository, new FileInfo(log4netConfig));

            _logger = LogManager.GetLogger(SunEcisConst.RepositoryName, SunEcisConst.LogName);
        }
    }
}
