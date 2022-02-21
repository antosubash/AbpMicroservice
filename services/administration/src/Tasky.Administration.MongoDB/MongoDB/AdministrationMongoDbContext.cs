using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tasky.Administration.MongoDB;

[ConnectionStringName(AdministrationDbProperties.ConnectionStringName)]
public class AdministrationMongoDbContext : AbpMongoDbContext, IAdministrationMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureAdministration();
    }
}
