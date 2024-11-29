using Xunit;

namespace Tasky.WebApp.EntityFrameworkCore;

[CollectionDefinition(WebAppTestConsts.CollectionDefinitionName)]
public class WebAppEntityFrameworkCoreCollection : ICollectionFixture<WebAppEntityFrameworkCoreFixture>
{

}
