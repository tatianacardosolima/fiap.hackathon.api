using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices;
using Fiap.Hackathon.Medicos.Application.Medicos.Services;
using Fiap.Hackathon.Medicos.Infrastructure.Persistence;

namespace Fiap.Hackathon.Medicos.API.Setup
{
    public static class MongoSetup
    {
        public static void AddMongo(this IServiceCollection services, ConfigurationManager configuration)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.Configure<MongoDbSettings>(configuration.GetSection("MongoDbSettings"));
            services.AddSingleton<MongoDbContext>();
        }
    }
}
