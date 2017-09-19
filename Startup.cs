using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PrimeNG.API.Service;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace PrimeNG.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<PrimeNGDbContext>(options =>
            options.UseSqlServer(("CustomersSqlServerConnectionString")));
            services.AddAutoMapper();
            services.AddCors(cfg =>
            {
                cfg.AddPolicy("MyApplication", bldr =>
                {
                    bldr.AllowAnyHeader()
                        .AllowAnyMethod();
                });

                cfg.AddPolicy("AnyGET", bldr =>
                {
                    bldr.AllowAnyHeader()
                       .AllowAnyMethod()
                        .AllowAnyOrigin();
                });
            });
            services.AddMvc(opt =>
            {
                opt.Filters.Add(new RequireHttpsAttribute());
            })
            .AddJsonOptions(opt =>
           {
               opt.SerializerSettings.ReferenceLoopHandling =
                 ReferenceLoopHandling.Ignore;
           });

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            loggerFactory.AddDebug();
            app.UseMvc();
        }
    }
}
