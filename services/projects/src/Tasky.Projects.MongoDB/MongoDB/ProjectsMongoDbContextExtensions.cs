using Volo.Abp;
using Volo.Abp.MongoDB;

namespace Tasky.Projects.MongoDB;

public static class ProjectsMongoDbContextExtensions
{
    public static void ConfigureProjects(
        this IMongoModelBuilder builder)
    {
        Check.NotNull(builder, nameof(builder));
    }
}
