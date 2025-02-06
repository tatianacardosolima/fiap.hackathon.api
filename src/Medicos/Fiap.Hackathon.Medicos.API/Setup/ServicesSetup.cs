using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices;
using Fiap.Hackathon.Medicos.Application.Medicos.Services;

namespace Fiap.Hackathon.Medicos.API.Setup
{
    public static class ServicesSetup
    {
        public static void AddServices(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IMedicoService, MedicoService>();
        }
    }
}
