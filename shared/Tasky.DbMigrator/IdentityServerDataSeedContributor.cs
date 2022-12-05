using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tasky.DbMigrator;

public class OpenIddictDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly OpenIddictDataSeeder _OpenIddictDataSeeder;

    public OpenIddictDataSeedContributor(OpenIddictDataSeeder OpenIddictDataSeeder)
    {
        _OpenIddictDataSeeder = OpenIddictDataSeeder;
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        await _OpenIddictDataSeeder.SeedAsync();
    }
}