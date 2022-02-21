using Volo.Abp.Data;
using Volo.Abp.MongoDB;

namespace Tasky.Administration.MongoDB;

[ConnectionStringName(AdministrationDbProperties.ConnectionStringName)]
public interface IAdministrationMongoDbContext : IAbpMongoDbContext
{
    /* Define mongo collections here. Example:
     * IMongoCollection<Question> Questions { get; }
     */
}
