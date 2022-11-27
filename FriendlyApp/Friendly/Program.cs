using Friendly.Database;
using Friendly.WebAPI;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddDbContext<FriendlyContext>(
           options => options.UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAspNetIdentity();

builder.Services.AddControllers();
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
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.MapControllers();

app.Run();
