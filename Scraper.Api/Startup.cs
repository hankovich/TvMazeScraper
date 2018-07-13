//  --------------------------------------------------------------------------------------------------------------------
//  <copyright file="Startup.cs" company="EPAM Systems">
//    2018
//  </copyright>
//  <summary>
//    Defines the Startup type.
//  </summary>
//  --------------------------------------------------------------------------------------------------------------------
namespace Scraper.Api
{
    using AutoMapper;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using Scraper.Api.Infrastructure.Configuration;
    using Scraper.Api.Infrastructure.Extensions;
    using Scraper.Api.Resources;
    using Scraper.Orm;

    public class Startup
    {
        private const string ConnectionStringName = "DefaultConnection";

        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;

            this.ApiInfoConfiguration = this.Configuration.GetSectionAs<ApiInfoConfiguration>();
            this.CacheConfiguration = this.Configuration.GetSectionAs<CacheConfiguration>();
            this.StringResources = this.Configuration.GetSectionAs<StringResources>();
        }

        public IConfiguration Configuration { get; }

        public StringResources StringResources { get; }

        public CacheConfiguration CacheConfiguration { get; }

        public ApiInfoConfiguration ApiInfoConfiguration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseResponseCaching();
            app.UseClientResponseCaching();

            app.UseMvc();

            app.UseSwagger(this.ApiInfoConfiguration);
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCaching();

            services.AddDbContext<ScraperContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString(ConnectionStringName)));

            services.AddMvc(this.CacheConfiguration);
            services.AddSwagger(this.ApiInfoConfiguration);

            services.AddAutoMapper();

            services.AddSingleton(this.CacheConfiguration);
            services.AddSingleton(this.ApiInfoConfiguration);
            services.AddSingleton(this.StringResources);
            services.AddSingleton(this.StringResources.PageStringResources);
            services.AddSingleton(this.StringResources.ValidationStringResources);

            services.ConfigureContainer();
        }
    }
}
