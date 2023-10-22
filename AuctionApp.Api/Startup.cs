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
using System.Linq;
using System.Threading.Tasks;

namespace AuctionApp.Api
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
            Log.Logger = new LoggerConfiguration()
              .MinimumLevel.Information()
              .MinimumLevel.Override("Microsoft", Serilog.Events.LogEventLevel.Warning)
              .WriteTo.File("logs/log-.txt", rollingInterval: RollingInterval.Day)
              .CreateLogger();

            // Register Serilog logger with DI
            services.AddLogging(builder => builder.AddSerilog());

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
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
            app.UseSerilogRequestLogging();
        }
    }
}
