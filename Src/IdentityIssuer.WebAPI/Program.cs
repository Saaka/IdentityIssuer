using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IdentityIssuer.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            WebDbInitializer.Initialize(host);
            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
