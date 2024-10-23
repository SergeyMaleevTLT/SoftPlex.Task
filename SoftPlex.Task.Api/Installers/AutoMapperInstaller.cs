using AutoMapper;
using SoftPlex.Task.Core.Domain.Models.Common;

namespace SoftPlex.Task.API.Installers
{
    public class AutoMapperInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var profiles = GetType().Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(Profile))).ToList();
            profiles.AddRange((typeof(BaseEntity).Assembly.GetTypes().Where(x => x.IsAssignableTo(typeof(Profile)))));
            
            var mappingConfig = new MapperConfiguration(mc => 
            {
                foreach(var type in profiles)
                    mc.AddProfile(type);
            });

            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}