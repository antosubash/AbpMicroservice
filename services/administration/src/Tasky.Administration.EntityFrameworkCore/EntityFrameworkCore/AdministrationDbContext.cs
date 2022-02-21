using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;
using Volo.Abp.PermissionManagement.EntityFrameworkCore;

namespace Tasky.Administration.EntityFrameworkCore;

[ConnectionStringName(AdministrationDbProperties.ConnectionStringName)]
public class AdministrationDbContext : AbpDbContext<AdministrationDbContext>, IAdministrationDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public AdministrationDbContext(DbContextOptions<AdministrationDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureAdministration();
        builder.ConfigurePermissionManagement();
        }
}
