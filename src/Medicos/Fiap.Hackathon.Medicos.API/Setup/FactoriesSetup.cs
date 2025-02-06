using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IFactories;
using Fiap.Hackathon.Medicos.Application.Medicos.Factories;

namespace Fiap.Hackathon.Medicos.API.Setup
{
    public static class FactoriesSetup
    {
        public static void AddFactories(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IMedicoFactory, MedicoFactory>();
        }
    }
}
