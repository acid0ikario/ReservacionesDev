using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessServices.Interfaces;
using BusinessServices.Services;
using DataAccess.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Repository;
using Repository.Interfaces;
using Repository.Services;
using WebApi.Middleware;

namespace WebApi
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
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddControllers();
            services.AddTokenAuthentication(Configuration);

            services.AddDbContext<ReservacionesContext>(x => x.UseSqlServer(Configuration.GetConnectionString("TestDevConn")));
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<ISalasRepository, SalasRepository>();
            services.AddScoped<IReservacionesRespository, ReservacionesRepository>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();

            //string secretKey = Configuration.GetValue<string>("DevSecretKey");
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ReservacionesContext>().AddDefaultTokenProviders();

            //services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //   .AddJwtBearer(options =>
            //    options.TokenValidationParameters = new TokenValidationParameters
            //    {
            //        ValidateIssuer = true,
            //        ValidateAudience = true,
            //        ValidateLifetime = true,
            //        ValidateIssuerSigningKey = true,
            //        ValidIssuer = "syntepro.com",
            //        ValidAudience = "syntepro.com",
            //        IssuerSigningKey = new SymmetricSecurityKey(
            //        Encoding.UTF8.GetBytes(secretKey)),
            //        ClockSkew = TimeSpan.Zero
            //    });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();
            //app.UseApiResponseAndExceptionWrapper(new AutoWrapperOptions { ShowStatusCode = true, EnableExceptionLogging = true });

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
