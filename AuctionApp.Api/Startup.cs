using AuctionApp.Api.Extensions;
using AuctionApp.Business.AccountServices;
using AuctionApp.Business.AuctionServices;
using AuctionApp.Business.Mapping;
using AuctionApp.Business.UserServices;
using AuctionApp.Domain.Enteties;
using AuctionApp.Infrastructure;
using AuctionApp.Infrastructure.Repositories;
using AuctionApp.Infrastructure.Repositories.AuctionRepositories;
using AuctionApp.Infrastructure.Repositories.UserRepositories;
using AutoMapper;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApp.Api
{
    public class Startup
    {
        private readonly IWebHostEnvironment _env;
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<AuctionAppDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddHangfire(configuration => configuration
                    .UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                 .AddEntityFrameworkStores<AuctionAppDbContext>()
                 .AddDefaultTokenProviders();


            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IAuctionService, AuctionService>();
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IUserService, UserService>();

            #region AutoMapper
            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new MappingProfiles()); });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            #endregion
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "My API",
                    Version = "v1",
                });
            });
            #region Configure Serilog
            //Configure Serilog to generate Log File
            Log.Logger = new LoggerConfiguration()
                 .MinimumLevel.Debug()
                 .WriteTo.File(Path.Combine(_env.ContentRootPath + "/Logs", "Log.txt"),
                  flushToDiskInterval: TimeSpan.FromDays(31))
                 .CreateLogger();
            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
            });

            GlobalConfiguration.Configuration.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection"));

            app.UseHangfireServer();
            HangfireJobs.RecurringJobs();
            #region Use Serilog
            loggerFactory.AddSerilog();
            #endregion
        }
    }
}
