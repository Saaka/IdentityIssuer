using IdentityIssuer.WebAPI.Services;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace IdentityIssuer.WebAPI
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateWebHostBuilder(args).Build();
            WebDbInitializer.Initialize(host);
            host.Run();
        }

        private static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
