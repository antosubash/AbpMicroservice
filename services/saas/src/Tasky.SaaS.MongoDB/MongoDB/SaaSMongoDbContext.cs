using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tasky.SaaS.MongoDB;

[ConnectionStringName(SaaSDbProperties.ConnectionStringName)]
public class SaaSMongoDbContext : AbpMongoDbContext, ISaaSMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureSaaS();
    }
}
