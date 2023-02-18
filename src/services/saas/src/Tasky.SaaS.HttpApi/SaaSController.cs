using Tasky.SaaS.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.SaaS;

public abstract class SaaSController : AbpControllerBase
{
    protected SaaSController()
    {
        LocalizationResource = typeof(SaaSResource);
    }
}