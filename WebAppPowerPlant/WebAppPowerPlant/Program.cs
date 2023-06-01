using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace WebAppPowerPlant
{
    public class Program
    {
        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                 .ConfigureWebHostDefaults(webBuilder =>
                 {
                     webBuilder.UseStartup<Startup>();
                     // webBuilder.ConfigureKestrel(options => options.ListenLocalhost(8888));
                 });

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }
    }
}