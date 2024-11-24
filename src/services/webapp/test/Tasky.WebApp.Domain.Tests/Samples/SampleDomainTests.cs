using Volo.Abp.Modularity;
using Xunit;

namespace Tasky.WebApp.Samples;

/* This is just an example test class.
 * Normally, you don't test code of the modules you are using
 * (like IdentityUserManager here).
 * Only test your own domain services.
 */
public abstract class SampleDomainTests<TStartupModule> : WebAppDomainTestBase<TStartupModule>
    where TStartupModule : IAbpModule
{
    [Fact]
    public void Should_Set_Email_Of_A_User()
    {
        Assert.Fail("This test is not implemented yet.");
    }
}
