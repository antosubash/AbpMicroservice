using Tasky.WebApp.Samples;
using Xunit;

namespace Tasky.WebApp.EntityFrameworkCore.Domains;

[Collection(WebAppTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<WebAppEntityFrameworkCoreTestModule>
{

}
