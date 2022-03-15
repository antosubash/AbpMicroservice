using System.Threading.Tasks;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;

namespace Tasky.DbMigrator;

public class IdentityServerDataSeedContributor : IDataSeedContributor, ITransientDependency
{
    private readonly IdentityServerDataSeeder _identityServerDataSeeder;

    public IdentityServerDataSeedContributor(IdentityServerDataSeeder identityServerDataSeeder)
    {
        _identityServerDataSeeder = identityServerDataSeeder;
    }


    public async Task SeedAsync(DataSeedContext context)
    {
        await _identityServerDataSeeder.SeedAsync();
    }
}