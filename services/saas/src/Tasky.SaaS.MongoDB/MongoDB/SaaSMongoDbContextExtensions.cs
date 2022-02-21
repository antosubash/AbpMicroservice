using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Tasky.SaaS.MongoDB;

public static class SaaSMongoDbContextExtensions
{
    public static void ConfigureSaaS(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
