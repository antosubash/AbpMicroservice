using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.TenantManagement.EntityFrameworkCore;

namespace Tasky.SaaS.EntityFrameworkCore;

public class SaaSHttpApiHostMigrationsDbContext : AbpDbContext<SaaSHttpApiHostMigrationsDbContext>
{
    public SaaSHttpApiHostMigrationsDbContext(DbContextOptions<SaaSHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureSaaS();
        modelBuilder.ConfigureTenantManagement();
    }
}
