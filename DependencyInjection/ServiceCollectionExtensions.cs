namespace GenericRepository.Core.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGenericRepository(this IServiceCollection services)
        {
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
            services.AddScoped<IBulkConfigProvider, DefaultBulkConfigProvider>();

            return services;
        }
    }
}
