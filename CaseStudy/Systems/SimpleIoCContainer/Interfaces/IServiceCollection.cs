using Case.Systems.SimpleIoCContainer.Enums;

namespace Case.Systems.SimpleIoCContainer.Interfaces
{
    public interface IServiceCollection
    {
        void Register<TService, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Transient)
          where TService : class
          where TImplementation : class, TService;
    }
}
