using Fiap.Hackathon.Medicos.API.Filters;
using Fiap.Hackathon.Medicos.API.Middlewares;
using Fiap.Hackathon.Medicos.API.Setup;
using Fiap.Hackathon.Medicos.Application.Abstractions.Autenticacao.IClients;
using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.IClients;
using Fiap.Hackathon.Medicos.Application.Autenticacao;
using Fiap.Hackathon.Medicos.Application.GeoLocalizacao.Clients;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

if (!builder.Environment.IsDevelopment())
    builder.WebHost.UseUrls("http://*:5011");

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins", policy =>
    {
        policy.AllowAnyOrigin()  // Permitir qualquer origem
              .AllowAnyMethod()  // Permitir qualquer método HTTP
              .AllowAnyHeader(); // Permitir qualquer cabeçalho
    });
});

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddControllers(options =>
//{
//    options.Filters.Add<ExceptionFilter>(); // Adiciona o filtro globalmente
//});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<LoggingDelegatingHandler>();

builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: false, reloadOnChange: true);
var configuration = builder.Configuration;

builder.Host.UseSerilog(SeriLogger.ConfigureLogger);

builder.Services.AddHttpClient<TokenValidationMiddleware>();

builder.Services.AddHttpClient<IGeoLocalizacaoClient, GeoLocalizacaoClient>(c =>
                c.BaseAddress = new Uri(configuration["GeoLocalizacaoURL"]!))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()                
                .AddPolicyHandler(PolicyHandler.GetCircuitBreakerPolicy())
                .AddPolicyHandler(PolicyHandler.GetRetryPolicy());

builder.Services.AddHttpClient<IAutenticacaoClient, AutenticacaoClient>(c =>
                c.BaseAddress = new Uri(configuration["Autenticacao"]!))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()
                .AddPolicyHandler(PolicyHandler.GetCircuitBreakerPolicy())
                .AddPolicyHandler(PolicyHandler.GetRetryPolicy());


builder.Services.AddMongo(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddFactories();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseCors("AllowAllOrigins");

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<TokenValidationMiddleware>();

//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
