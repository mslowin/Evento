using Evento.Evento.Core.Repositories;
using Evento.Evento.Infrastructure.Mappers;
using Evento.Evento.Infrastructure.Repositories;
using Evento.Evento.Infrastructure.Services;
using Evento.Evento.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Owin.Logging;
using System.Text;
using Evento.Framework;

var builder = WebApplication.CreateBuilder(args);
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.WriteIndented = true);
builder.Services.AddMemoryCache();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddScoped<ITicketService, TicketService>();
builder.Services.AddScoped<IDataInitializer, DataInitializer>();
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("jwt"));
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("app"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var config = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config.Issuer,
        ValidateAudience = false,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwt:key").Value))
    };
});
builder.Services.AddAuthorization(x => x.AddPolicy("HasAdminRole", p => p.RequireRole("admin")));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

SeedData(app);
app.UseErrorHandler();
app.Logger.LogInformation("Starting the app");
app.Run();


void SeedData(IApplicationBuilder app)
{
    var settings = app.ApplicationServices.GetService<IOptions<AppSettings>>();
    if (settings!.Value.SeedData)
    {
        var scope = app.ApplicationServices.CreateScope();
        var service = scope.ServiceProvider.GetService<IDataInitializer>();
        service!.SeedAsync();
    }
}
