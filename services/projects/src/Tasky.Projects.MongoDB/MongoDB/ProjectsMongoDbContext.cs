using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tasky.Projects.MongoDB;

[ConnectionStringName(ProjectsDbProperties.ConnectionStringName)]
public class ProjectsMongoDbContext : AbpMongoDbContext, IProjectsMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureProjects();
    }
}
