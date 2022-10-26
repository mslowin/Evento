using Evento.Evento.Core.Repositories;
using Evento.Evento.Infrastructure.Mappers;
using Evento.Evento.Infrastructure.Repositories;
using Evento.Evento.Infrastructure.Services;
using Evento.Evento.Infrastructure.Settings;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc().AddJsonOptions(x => x.JsonSerializerOptions.WriteIndented = true);
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IEventService, EventService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IJwtHandler, JwtHandler>();
builder.Services.AddSingleton(AutoMapperConfig.Initialize());
//var jwtSettings = builder.Configuration.GetSection("jwt").Get<IOptions<JwtSettings>>();      
builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("jwt"));

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{
    var config = builder.Services.BuildServiceProvider().GetRequiredService<IOptions<JwtSettings>>().Value;
    //var test = Encoding.UTF8.GetBytes(builder.Configuration[config.Key]);           // <--- tu b³¹d jakiœ
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = config.Issuer,
        //ValidateIssuer = true,
        ValidateAudience = false,
        //ValidateLifetime = false,
        //ValidateIssuerSigningKey = true,
        //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[config.Key]))
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("jwt:key").Value))
    };
});
builder.Services.AddAuthorization();


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

app.Run();
