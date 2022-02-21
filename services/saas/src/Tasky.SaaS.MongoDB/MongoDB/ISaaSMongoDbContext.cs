using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tasky.SaaS.MongoDB;

[ConnectionStringName(SaaSDbProperties.ConnectionStringName)]
public interface ISaaSMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
