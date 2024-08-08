using Case.Systems.SimpleIoCContainer.Enums;
using Case.Systems.SimpleIoCContainer.Interfaces;
using Case.Systems.SingletonSystem;
using IServiceProvider = Case.Systems.SimpleIoCContainer.Interfaces.IServiceProvider;

namespace Case.Systems.SimpleIoCContainer.Core
{
    public class IoCContainer : Singleton<IoCContainer>, IServiceCollection, IServiceProvider
    {
        private readonly Dictionary<Type, ServiceDescriptor> services = new Dictionary<Type, ServiceDescriptor>();
        private readonly Dictionary<Type, object> resolvedServices = new Dictionary<Type, object>();

        public void Register<TService, TImplementation>(ServiceLifetime lifetime = ServiceLifetime.Transient)
            where TService : class
            where TImplementation : class, TService
        {
            services[typeof(TService)] = new ServiceDescriptor(typeof(TService), typeof(TImplementation), lifetime);
        }

        public object GetService(Type serviceType)
        {
            return Resolve(serviceType);
        }

        public TService GetService<TService>() where TService : class
        {
            return (TService) Resolve(typeof(TService));
        }

        private object Resolve(Type serviceType)
        {
            if (resolvedServices.ContainsKey(serviceType))
            {
                return resolvedServices[serviceType];
            }

            if (!services.ContainsKey(serviceType))
            {
                throw new Exception($"Service of type {serviceType.Name} is not registered.");
            }

            var descriptor = services[serviceType];
            object resolvedService;

            if (descriptor.Lifetime == ServiceLifetime.Singleton)
            {
                resolvedService = descriptor.Implementation ??= CreateInstance(descriptor.ImplementationType);
            }
            else
            {
                resolvedService = CreateInstance(descriptor.ImplementationType);
            }

            resolvedServices[serviceType] = resolvedService;
            return resolvedService;
        }

        private object CreateInstance(Type type)
        {
            var constructor = type.GetConstructors()[0];
            var parameters = constructor.GetParameters();

            if (parameters.Length == 0)
            {
                return Activator.CreateInstance(type);
            }

            var parameterImplementations = new List<object>();
            foreach (var parameter in parameters)
            {
                var parameterImplementation = Resolve(parameter.ParameterType);
                parameterImplementations.Add(parameterImplementation);
            }

            return constructor.Invoke(parameterImplementations.ToArray());
        }
    }
}
