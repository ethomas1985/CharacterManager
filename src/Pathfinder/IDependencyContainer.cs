namespace Pathfinder
{
    public interface IDependencyContainer
    {
        IDependencyContainer Register<TInterface, TClass>() where TInterface : class where TClass : class, TInterface;
        IDependencyContainer RegisterInstance<TInterface, TClass>(TClass pSingleton) where TInterface : class where TClass : class, TInterface;
        T Get<T>() where T : class;
    }
}
