using SoftPlex.Task.Core.Infrastructure.Auth;

namespace SoftPlex.Task.API.Installers;

public class AuthJwtInstaller : IInstaller
{
    public void InstallServices(IServiceCollection services, IConfiguration configuration)
    {
        var section = configuration.GetSection("JwtSettings");
        var settings = section.Get<JwtSettings>();
        if (settings == null)
        {
            throw new ArgumentNullException("JwtSettings section not found");
        }

        services.AddSingleton(settings);
        services.AddAuthJwt(settings);
    }
}