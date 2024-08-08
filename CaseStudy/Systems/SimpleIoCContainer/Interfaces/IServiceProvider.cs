namespace Case.Systems.SimpleIoCContainer.Interfaces
{
    public interface IServiceProvider
    {
        TService GetService<TService>() where TService : class;

        object GetService(Type serviceType);
    }
}
