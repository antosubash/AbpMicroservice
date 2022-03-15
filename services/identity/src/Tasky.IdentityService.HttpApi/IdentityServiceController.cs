using Tasky.IdentityService.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.IdentityService;

public abstract class IdentityServiceController : AbpControllerBase
{
    protected IdentityServiceController()
    {
        LocalizationResource = typeof(IdentityServiceResource);
    }
}