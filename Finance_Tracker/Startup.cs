using Finance_Tracker.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Finance_Tracker.Services;
using Finance_Tracker.IdentityAuth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Finance_Tracker
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        public Startup(IConfiguration Configuration)
        {
            _configuration = Configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            services.AddDbContext<FinanceTrackerDB>(options =>
            options.UseSqlServer(_configuration.GetConnectionString("FinanceTrackerDev")));

            services.AddControllers();

            // For Identity
            services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<FinanceTrackerDB>()
            .AddDefaultTokenProviders();
            // Adding Authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:ValidAudience"],
                    ValidIssuer = _configuration["JWT:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]))
                };
            });
            //The AddScoped method registers the service with a scoped lifetime
            services.AddScoped<IInvoiceService, InvoiceService>();
            services.AddScoped<IPartnerService, PartnerService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IInvoiceTypeService, InvoiceTypeService>();
            services.AddScoped<ICostInvoiceService, CostInvoiceService>();
            services.AddScoped<ICompanyService, CompanyService>();
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
