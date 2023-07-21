using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using PortfolioWebsite.DAO;
using PortfolioWebsite.DAO.Interfaces;
using PortfolioWebsite.Interfaces;
using PortfolioWebsite.Models;
using PortfolioWebsite.Security;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace PortfolioWebsite
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
            services.AddControllers();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });


            // Register ITokenGenerator service
            string jwtSecret = Configuration["JwtSecret"] ?? "default value";
            services.AddSingleton<ITokenGenerator>(new JwtGenerator(jwtSecret));

            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(Configuration["JwtSecret"]);
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
                    ValidateAudience = false,
                    NameClaimType = "name"
                };
            });

            // Dependency Injection configuration
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IPostDAO, PostSqlDAO>();
            services.AddTransient<ICategoryDAO, CategorySqlDAO>();
            services.AddTransient<ICommentDAO, CommentSqlDAO>();
            services.AddTransient<IProjectDAO, ProjectSqlDAO>();
            services.AddTransient<IUserDAO, UserSqlDAO>();

            // Configure HTTPS
            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status307TemporaryRedirect;
              //  options.HttpsPort = 7291; // Replace this with the actual HTTPS port you want to use
            //});

            // Add Razor Pages support
            services.AddRazorPages();

            // Ensure each endpoint works successfully for each controller that is talking to your DB
            //services.AddControllers(options =>
            //{
               // options.ValidateAntiForgeryToken = false;
            //});
       }


        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();
            app.UseAuthentication();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapRazorPages(); // Map Razor Pages endpoints
            });
        }
    }
}
