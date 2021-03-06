using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using RazzorPagesExample.Model;
using RazzorPagesExample.Models;
using System;

namespace RazzorPagesExample
{
    public class Startup
    {

        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }

        public IConfiguration Configuration { get; }

       

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddDistributedMemoryCache();

            services.AddSession(options =>
            {
                // Set a short timeout for easy testing.
                options.IdleTimeout = TimeSpan.FromMinutes(10);
                options.Cookie.HttpOnly = true;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            

           
            services.AddSingleton<string>(Configuration.GetConnectionString("RazzorPagesExampleContext"));
            services.AddTransient<IProductRepository, ProductRepository>();

            services.AddTransient<IProductRazorRepository<Product>, ProductRazorRepository>();




            services.AddDbContext<dbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RazzorPagesExampleContext")));

            services.AddDbContext<RazzorPagesExampleContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("RazzorPagesExampleContext")));


            //services.AddSingleton<ProductRepository>(new ProductRepository(Configuration["DefaultConnection:ConnectionString"]));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseMvc();
        }
    }
}
