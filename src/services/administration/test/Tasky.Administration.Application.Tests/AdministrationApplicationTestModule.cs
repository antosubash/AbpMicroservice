using Volo.Abp.Modularity;

namespace Tasky.Administration;

[DependsOn(typeof(AdministrationApplicationModule))]
[DependsOn(typeof(AdministrationDomainTestModule))]
public class AdministrationApplicationTestModule : AbpModule { }
