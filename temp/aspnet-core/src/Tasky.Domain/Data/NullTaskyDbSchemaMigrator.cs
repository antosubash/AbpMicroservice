using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;

namespace Tasky.Data;

/* This is used if database provider does't define
 * ITaskyDbSchemaMigrator implementation.
 */
public class NullTaskyDbSchemaMigrator : ITaskyDbSchemaMigrator, ITransientDependency
{
    public Task MigrateAsync()
    {
        return Task.CompletedTask;
    }
}
