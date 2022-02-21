using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.Identity.EntityFrameworkCore;
using Volo.Abp.IdentityServer.EntityFrameworkCore;

namespace Tasky.IdentityService.EntityFrameworkCore;

public class IdentityServiceHttpApiHostMigrationsDbContext : AbpDbContext<IdentityServiceHttpApiHostMigrationsDbContext>
{
    public IdentityServiceHttpApiHostMigrationsDbContext(DbContextOptions<IdentityServiceHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureIdentityService();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureIdentityServer();
    }
}
