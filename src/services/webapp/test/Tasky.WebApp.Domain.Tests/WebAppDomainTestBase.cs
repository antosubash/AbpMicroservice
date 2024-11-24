using Volo.Abp.Modularity;

namespace Tasky.WebApp;

/* Inherit from this class for your domain layer tests. */
public abstract class WebAppDomainTestBase<TStartupModule> : WebAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
