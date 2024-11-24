using Tasky.WebApp.Samples;
using Xunit;

namespace Tasky.WebApp.EntityFrameworkCore.Applications;

[Collection(WebAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<WebAppEntityFrameworkCoreTestModule>
{

}
