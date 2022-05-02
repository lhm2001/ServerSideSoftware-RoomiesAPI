using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Roomies.API.Domain.Persistence.Contexts;
using Roomies.API.Domain.Persistence.Repositories;
using Roomies.API.Domain.Repositories;
using Roomies.API.Domain.Services;
using Roomies.API.Exceptions;
using Roomies.API.Persistence.Repositories;
using Roomies.API.Publication.Domain.Persistence.Repositories;
using Roomies.API.Publication.Domain.Services;
using Roomies.API.Publication.Persistence.Repositories;
using Roomies.API.Publication.Services;
using Roomies.API.Services;
using Roomies.API.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Roomies.API
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

            // Add CORS Support
            services.AddCors();

            services.AddControllers();

            // DbContext Configuration
            services.AddDbContext<AppDbContext>(options =>
            {
                //options.UseMySQL(Configuration.GetConnectionString("DefaultConnection"));
                options.UseMySQL(Configuration.GetConnectionString("SmarterAspMySqlConnection"));

            });

            // AppSettings Section Reference
            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            // JSON Web Token Authentication Configuration
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);

            // Authentication Service Configuration
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            // Dependency Injection Configuration
            services.AddScoped<IFavouritePostRepository, FavouritePostRepository>();
            services.AddScoped<ILandlordRepository, LandlordRepository>();
            services.AddScoped<ILeaseholderRepository, LeaseholderRepository>();
            services.AddScoped<IPaymentMethodRepository,PaymentMethodRepository>();
            services.AddScoped<IPlanRepository,PlanRepository>();
            services.AddScoped<IPostRepository,PostRepository>();
            services.AddScoped<IReviewRepository,ReviewRepository>();
            services.AddScoped<IProfilePaymentMethodRepository,ProfilePaymentMethodRepository>();
            services.AddScoped<IRuleRepository, RuleRepository>();
            services.AddScoped<IProfileRepository, ProfileRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<IFavouritePostService, FavouritePostService>();
            services.AddScoped<ILandlordService, LandlordService>();
            services.AddScoped<ILeaseholderService, LeaseholderService>();
            services.AddScoped<IPaymentMethodService,PaymentMethodService >();
            services.AddScoped<IPlanService,PlanService >();
            services.AddScoped<IPostService,PostService >();
            services.AddScoped<IReviewService,ReviewService > ();
            services.AddScoped<IProfilePaymentMethodService,ProfilePaymentMethodService>();
            services.AddScoped<IRuleService, RuleService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IUserService, UserService>();

            // Endpoints Case Conventions Configuration

            services.AddRouting(options => options.LowercaseUrls = true);

            // AutoMapper initialization
            services.AddAutoMapper(typeof(Startup));


            // Documentation Setup
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Roomies.API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Roomies.API v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // CORS Configuration
            app.UseCors(x => x.SetIsOriginAllowed(origin => true)
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials());

            // Authentication Support
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseMiddleware<ExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
