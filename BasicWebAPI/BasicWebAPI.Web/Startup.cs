using BasicWebAPI.Core;
using BasicWebAPI.Core.Entities;
using BasicWebAPI.Core.Interfaces;
using BasicWebAPI.Core.Interfaces.Services;
using BasicWebAPI.Core.Services;
using BasicWebAPI.Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace BasicWebAPI.Web
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
            PickImplementations(services);
            ConfigureAuthentication(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        private void ConfigureAuthentication(IServiceCollection services)
        {
            services.AddAuthentication(
                option =>
                {
                    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    option.DefaultForbidScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.RequireHttpsMetadata = false;
                    options.SaveToken = true;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["TokenConfiguration:Issuer"],
                        ValidAudience = Configuration["TokenConfiguration:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(Configuration["TokenConfiguration:SecurityKey"]))
                    };
                }
            );
        }

        private void PickImplementations(IServiceCollection services)
        {
            services.AddSingleton<IConfiguration>(Configuration);

            var connectionString = Configuration.GetSection("ConnectionStrings:HomeInventory").Value;
            services.AddTransient<DbContext, BasicWebAPIDbContext>();
            services.AddDbContext<BasicWebAPIDbContext>(opt =>
                    opt.UseSqlServer(connectionString));
            services.AddTransient(typeof(IRepository<>), typeof(DatabaseRepository<>));
            services.AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<BasicWebAPIDbContext>();

            services.AddTransient<IExampleService, ExampleService>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BasicWebAPIDbContext context, UserManager<User> userManager)
        {
            MapperInitializer.Initialize();

            if (env.IsDevelopment())
            {
                Seed.InitializeHomeInventoryDb(context, userManager);
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
