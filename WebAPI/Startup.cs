using AutoMapper;
using Business.Abstract;
using Business.Concrete;
using Core.Extensions;
using Core.Utilities.Security.Token;
using Core.Utilities.Security.Token.jwt;
using DataAccess.Abstract;
using DataAccess.Concrete.Contexts;
using DataAccess.Concrete.EntityFramework;
using Entities.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI
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
            services.AddDbContext<WebAPIContext>(opts => opts.UseSqlServer("Data Source = DESKTOP - AOMM71G; Initial Catalog = WebAPIContextDb; Integrated Security = true",
                options => options.MigrationsAssembly("DataAccess").MigrationsHistoryTable(HistoryRepository.DefaultTableName, "dbo")));

            services.AddControllers();
            services.AddCustomSwagger();
            services.AddCustomJwtToken(Configuration);

            services.AddAutoMapper(typeof(Mapping));

            //var mapperConfi = new MapperConfiguration(x =>
            //{
            //    x.AddProfile(new Mapping());
            //});
            //var mapper = mapperConfi.CreateMapper();
            //services.AddSingleton(mapper);

            

            services.AddTransient<IUserDal, EfUserDal>();
            services.AddTransient<IUserService, UserManager>();
            services.AddTransient<ITokenService, JwtTokenService>();
            services.AddScoped<IAuthService, AuthManager>();



        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseCustomSwagger();

            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
