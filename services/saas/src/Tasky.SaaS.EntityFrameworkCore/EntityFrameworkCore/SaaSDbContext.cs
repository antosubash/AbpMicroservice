using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.SaaS.EntityFrameworkCore;

[ConnectionStringName(SaaSDbProperties.ConnectionStringName)]
public class SaaSDbContext : AbpDbContext<SaaSDbContext>, ISaaSDbContext
{
    /* Add DbSet for each Aggregate Root here. Example:
     * public DbSet<Question> Questions { get; set; }
     */

    public SaaSDbContext(DbContextOptions<SaaSDbContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ConfigureSaaS();
    }
}
