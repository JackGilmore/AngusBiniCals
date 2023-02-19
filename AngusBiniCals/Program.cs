using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace AngusBiniCals
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseUrls("http://*:9000");
                    webBuilder.UseStartup<Startup>();
                });
    }
}
