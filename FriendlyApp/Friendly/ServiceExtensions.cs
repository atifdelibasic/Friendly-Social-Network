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
            services.Configure<IdentityOptions>(options =>
            {
                options.Lockout.AllowedForNewUsers = true;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                options.Lockout.MaxFailedAccessAttempts = 3;

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

            services.AddIdentity<User, IdentityRole<int>>()
               .AddDefaultTokenProviders()
               .AddRoles<IdentityRole<int>>()
               .AddEntityFrameworkStores<FriendlyContext>()
               .AddPasswordValidator<EmailPasswordValidator>()
               .AddPasswordValidator<CommonPasswordValidator<User>>();

           
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
                    ValidateLifetime = true
                };
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddTransient<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IHobbyService, HobbyService>();
            services.AddTransient<IHobbyCategoryService, HobbyCategoryService>();
            services.AddTransient<IPostService, PostService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
        }
    }
}
