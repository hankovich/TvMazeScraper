//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Program.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Program type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args).UseStartup<Startup>();

        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }
    }
}
