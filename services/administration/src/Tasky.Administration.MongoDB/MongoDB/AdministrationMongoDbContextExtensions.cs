using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Tasky.Administration.MongoDB;

public static class AdministrationMongoDbContextExtensions
{
    public static void ConfigureAdministration(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
