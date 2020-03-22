using System.Threading.Tasks;
using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IdentityIssuer.WebAPI
{
    public static class Program
    {
        public  static async Task Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            await WebApplicationInitializer.InitializeAsync(host);
            await host.RunAsync();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
