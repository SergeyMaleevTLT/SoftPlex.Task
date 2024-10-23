using SoftPlex.Task.Core.Infrastructure.Data;
using SoftPlex.Task.Core.Infrastructure.Data.Context;

namespace SoftPlex.Task.API.Installers;

public class DbInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("ConnectionStrings");
        var settings = section.Get<ConnectionStringsSection>();
        if (settings == null)
        {
                throw new ArgumentNullException("ConnectionStrings section not found");
        }

        services.AddDataRepository(settings);
    }
}