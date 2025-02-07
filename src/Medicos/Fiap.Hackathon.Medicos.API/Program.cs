using Fiap.Hackathon.Medicos.API.Filters;
using Fiap.Hackathon.Medicos.API.Setup;
using Fiap.Hackathon.Medicos.Application.Abstractions.GeoLocalizacao.IClients;
using Fiap.Hackathon.Medicos.Application.GeoLocalizacao.Clients;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

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

builder.Services.AddHttpClient<IGeoLocalizacaoClient, GeoLocalizacaoClient>(c =>
                c.BaseAddress = new Uri(configuration["GeoLocalizacaoURL"]!))
                .AddHttpMessageHandler<LoggingDelegatingHandler>()                
                .AddPolicyHandler(PolicyHandler.GetCircuitBreakerPolicy())
                .AddPolicyHandler(PolicyHandler.GetRetryPolicy());


builder.Services.AddMongo(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddFactories();
builder.Services.AddServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
