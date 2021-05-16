using AccountManager.Api;
using AccountManager.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AccountManager.Helpers;
using Microsoft.Extensions.Options;

namespace AccountManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            
        }
        
        private readonly string v1 = "v1";
        private readonly string appTitle = "The Account Manager API";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));
            DependencyInjections.Register(services);
            services.AddDbContext<AccountManagerContext>(
                o => o.UseSqlServer(Configuration.GetConnectionString(nameof(AccountManagerContext))));
            services.AddCors();
            var sharedKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(Configuration["AppSettings:Secret"]));
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;

                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = sharedKey,

                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                };
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc(v1, new OpenApiInfo { Title = appTitle, Version = v1 });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(o => o
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            );
            app.UseHttpsRedirection();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{appTitle} {v1}");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
