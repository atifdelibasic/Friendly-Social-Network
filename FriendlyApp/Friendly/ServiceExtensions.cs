using Friendly.Database;
using Friendly.Service;
using Friendly.Service.Brokers;
using Friendly.WebAPI.PasswordValidator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using publisher_api.Services;
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

            services.AddHttpContextAccessor();

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
                    RequireExpirationTime = false,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });
        }

        public static void ConfigureServices(this IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton(typeof(ILogger<>), typeof(Logger<>));

            services.AddTransient<IRoleService, RoleService>();
            services.AddScoped<IUserService, UserService>();
            services.AddTransient<IEmailService, EmailService>();
            services.AddTransient<IHobbyService, HobbyService>();
            services.AddTransient<IHobbyCategoryService, HobbyCategoryService>();
            services.AddTransient<IPostService, PostService>();
            services.AddScoped<IFriendshipService, FriendshipService>();
            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ILikeService, LikeService>();
            services.AddScoped<IChatService, ChatService>();
            services.AddScoped<IBlockService, BlockService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<HttpAccessorHelperService>();
            services.AddSingleton<IConnectionService<string>, ConnectionService<string>>();

            //services.AddScoped<IMessageProducer, MessageProducer>();
            services.AddSingleton<IMessageService, MessageService>();
        }
    }
}
