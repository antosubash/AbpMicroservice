using Volo.Abp.Modularity;

namespace Tasky.WebApp.Samples;

// This is just an example test class.
public abstract class SampleAppServiceTests<TStartupModule> : WebAppApplicationTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{

}
