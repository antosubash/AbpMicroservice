using Microsoft.EntityFrameworkCore;
using Volo.Abp.Data;
using Volo.Abp.EntityFrameworkCore;

namespace Tasky.WebApp.EntityFrameworkCore;

[ConnectionStringName("Default")]
public class WebAppDbContext(DbContextOptions<WebAppDbContext> options) : AbpDbContext<WebAppDbContext>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        /* Include modules to your migration db context */

        /* Configure your own tables/entities inside here */

        //builder.Entity<YourEntity>(b =>
        //{
        //    b.ToTable(WebAppConsts.DbTablePrefix + "YourEntities", WebAppConsts.DbSchema);
        //    b.ConfigureByConvention(); //auto configure for the base class props
        //    //...
        //});
    }
}
