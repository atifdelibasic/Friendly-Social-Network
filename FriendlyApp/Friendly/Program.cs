using Friendly.Database;
using Friendly.WebAPI;
using Friendly.WebAPI.Filter;
using Hangfire;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<FriendlyContext>(
           options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(option =>
{
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "bearer",
    });

    option.EnableAnnotations();

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

builder.Services.ConfigureAspNetIdentity();

builder.Services.AddControllers(x =>
{
    x.Filters.Add<ErrorFilter>();
});

builder.Services.ConfigureAuthentication(configuration);

builder.Services.ConfigureServices();

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddHangfire(x => x.UseSqlServerStorage(configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddHangfireServer();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseDeveloperExceptionPage();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();

app.Run();
