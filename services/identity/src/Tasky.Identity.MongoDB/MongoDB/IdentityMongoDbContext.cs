using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tasky.Identity.MongoDB;

[ConnectionStringName(IdentityDbProperties.ConnectionStringName)]
public class IdentityMongoDbContext : AbpMongoDbContext, IIdentityMongoDbContext
{
    /* Add mongo collections here. Example:
     * public IMongoCollection<Question> Questions => Collection<Question>();
     */

    protected override void CreateModel(IMongoModelBuilder modelBuilder)
    {
        base.CreateModel(modelBuilder);

        modelBuilder.ConfigureIdentity();
    }
}
