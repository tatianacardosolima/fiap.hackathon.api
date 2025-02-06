using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IRepositories;
using Fiap.Hackathon.Medicos.Application.Abstractions.Medicos.IServices;
using Fiap.Hackathon.Medicos.Application.Medicos.Services;
using Fiap.Hackathon.Medicos.Infrastructure.Repositories;

namespace Fiap.Hackathon.Medicos.API.Setup
{
    public static class RepositoriesSetup
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            if (services == null)
                throw new ArgumentNullException(nameof(services));

            services.AddScoped<IMedicoRepository, MedicoRepository>();
        }
    }
}
