using System.Threading.Tasks;

namespace Tasky.Data;

public interface ITaskyDbSchemaMigrator
{
    Task MigrateAsync();
}
