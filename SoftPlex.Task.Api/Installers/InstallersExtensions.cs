namespace SoftPlex.Task.API.Installers
{
    public static class InstallersExtensions
    {
        /// <summary>
        /// Регистрирует все сервисы проекта 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration">Конфигурации проекта</param>
        public static void InstallServicesInAssembly(this IServiceCollection services, IConfiguration configuration)
        {
            var installers = typeof(Program).Assembly.ExportedTypes.Where(
                    x => typeof(IInstaller).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract)
                .Select(Activator.CreateInstance).Cast<IInstaller>().ToList();

            installers.ForEach(installer => installer.InstallServices(services, configuration));
        }
    }
}