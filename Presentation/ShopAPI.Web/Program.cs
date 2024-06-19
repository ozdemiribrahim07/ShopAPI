using Amazon;
using Amazon.Extensions.NETCore.Setup;
using Amazon.Runtime;
using Amazon.S3;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using ShopAPI.Application;
using ShopAPI.Application.Validators.Product;
using ShopAPI.Infrastructure;
using ShopAPI.Infrastructure.Filters;
using ShopAPI.Infrastructure.Services.Storage.Storage.AWS;
using ShopAPI.Infrastructure.Services.Storage.Storage.Local;
using ShopAPI.Persistance;
using ShopAPI.Web.AllConfigurations.ColumnWriters;
using System.Security.Claims;
using System.Text;
using ShopAPI.SignalR;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(opt => opt.Filters.Add<ValidationFilter>())
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>()).ConfigureApiBehaviorOptions(opt => opt.SuppressModelStateInvalidFilter = true);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(opt => opt.AddDefaultPolicy(b => b.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod().AllowCredentials()));

builder.Services.AddDefaultAWSOptions(builder.Configuration.GetAWSOptions());
builder.Services.AddAWSService<IAmazonS3>();
builder.Services.AddPersistance();
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddStorage<AwsStorage>();
builder.Services.AddSignalRServices();

Logger logger = new LoggerConfiguration()
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(
    builder.Configuration.GetConnectionString("PostgreSql"),
    "Logs",
    needAutoCreateTable: true,
    columnOptions : new Dictionary<string, ColumnWriterBase>
    {
        {"message", new RenderedMessageColumnWriter() },
        {"message_template", new MessageTemplateColumnWriter() },
        {"level", new LevelColumnWriter() },
        {"time_stamp", new TimestampColumnWriter() },
        {"exception", new ExceptionColumnWriter() },
        {"log_event", new LogEventSerializedColumnWriter() },
        {"user_name", new UsernameColumnWriter() }
    })
    .Enrich.FromLogContext()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.Seq(builder.Configuration["Seq:Url"])
    .CreateLogger();

builder.Host.UseSerilog(logger);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", opt =>
{
    opt.TokenValidationParameters = new()
    {
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes
        (builder.Configuration["Token:SecurityKey"])),

        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) =>
        {
            return expires != null ? expires > DateTime.UtcNow : false;
        },

        NameClaimType = ClaimTypes.Name
    };

});

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var app = builder.Build();



if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseSerilogRequestLogging();

app.UseCors();
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.Use(async(context, next) =>
{
   var username = context.User?.Identity?.IsAuthenticated != null && true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);

    await next();
});

app.UseStaticFiles();
app.MapControllers();

app.MapHub();

app.Run();
