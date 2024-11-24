using Volo.Abp.Modularity;

namespace Tasky.WebApp;

public abstract class WebAppApplicationTestBase<TStartupModule> : WebAppTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
