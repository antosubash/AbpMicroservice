using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.Identity.EntityFrameworkCore;

public class IdentityHttpApiHostMigrationsDbContext : AbpDbContext<IdentityHttpApiHostMigrationsDbContext>
{
    public IdentityHttpApiHostMigrationsDbContext(DbContextOptions<IdentityHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureIdentity();
    }
}
