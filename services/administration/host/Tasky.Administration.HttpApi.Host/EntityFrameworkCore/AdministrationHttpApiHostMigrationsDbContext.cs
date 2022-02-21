using Microsoft.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.Administration.EntityFrameworkCore;

public class AdministrationHttpApiHostMigrationsDbContext : AbpDbContext<AdministrationHttpApiHostMigrationsDbContext>
{
    public AdministrationHttpApiHostMigrationsDbContext(DbContextOptions<AdministrationHttpApiHostMigrationsDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureAdministration();
    }
}
