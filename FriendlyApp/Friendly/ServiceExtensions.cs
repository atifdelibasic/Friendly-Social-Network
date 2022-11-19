using Friendly.Database;
using Friendly.Service;
using Friendly.WebAPI.PasswordValidator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Friendly.WebAPI
{
    public static class ServiceExtensions
    {
        public static void ConfigureAspNetIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<int>>()
               //.AddRoles<IdentityRole<int>>()
               .AddEntityFrameworkStores<FriendlyContext>()
               .AddDefaultTokenProviders()
               .AddPasswordValidator<EmailPasswordValidator>()
               .AddPasswordValidator<CommonPasswordValidator<User>>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;


                // Email settings
                options.User.RequireUniqueEmail = true;
                options.SignIn.RequireConfirmedEmail = true;
            });
        }

        public static void ConfigureAuthentication(this IServiceCollection services, ConfigurationManager configuration)
        {

            services.AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidAudience = configuration["AuthSettings:ValidAudience"],
                    ValidIssuer = configuration["AuthSettings:ValidIssuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["AuthSettings:Key"])),
                    // RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = false
                };
            });
        }

        public static void ConfigureServices(this IServiceCollection service)
        {
            service.AddTransient<IRoleService, RoleService>();
            service.AddTransient<IUserService, UserService>();
            service.AddTransient<IEmailService, EmailService>();
            service.AddTransient<IHobbyService, HobbyService>();
            service.AddTransient<IHobbyCategoryService, HobbyCategoryService>();
        }
    }
}
