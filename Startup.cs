using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using FirstBlog.Data;
using Microsoft.EntityFrameworkCore;
using FirstBlog.Data.Repository;
using Microsoft.AspNetCore.Identity;
using FirstBlog.Models;

namespace FirstBlog
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            this._config = config;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options.UseSqlServer(_config["DefaultConnection"]));

            services.AddIdentity<User, IdentityRole>(options =>
                {
                    options.Password.RequireNonAlphanumeric = false;
                    options.Password.RequiredLength = 8;
            })
                .AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/Auth/Login";
            });

            services.AddTransient<IRepository, Repository>();

            services.AddMvc(options => options.EnableEndpointRouting=false);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles();

            //app.UseRouting();

            app.UseAuthentication();

            app.UseMvcWithDefaultRoute();
        }
    }
}
