using Authentication._2FA.Shared.Interfaces;
using Authentication._2FA.Shared.Models;
using Authentication._2FA.Ioc;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Authentication._2FA.Shared.Constants;
using Authentication._2FA.Shared.Constants;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http.Features;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddScoped<IActionResultConverter, ActionResultConverter>();
builder.Services.AddInfrastructure(builder.Configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = false;
    options.LowercaseQueryStrings = false;
    options.LowercaseQueryStrings = false;
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:key"])),
            ClockSkew = TimeSpan.Zero
        };

        options.Events = new JwtBearerEvents();
        options.Events.OnChallenge = context =>
        {
            // Skip the default logic.
            context.HandleResponse();


            var payload = new ErrorMessage[] { new ErrorMessage(ErrorCodes.AuthenticateError, "Erro ao realizar autenticação") };
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 401;

            return context.Response.WriteAsync(JsonConvert.SerializeObject(payload));
        };

    });
builder.Services.AddControllers().AddNewtonsoftJson(opt =>
{
    opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
    opt.SerializerSettings.Converters.Add(new StringEnumConverter());
});

builder.Services.Configure<FormOptions>(o =>
{
    o.ValueLengthLimit = int.MaxValue;
    o.MultipartBodyLengthLimit = int.MaxValue;
    o.MemoryBufferThreshold = int.MaxValue;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
