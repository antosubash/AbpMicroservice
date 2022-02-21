using Tasky.Identity.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.Identity;

public abstract class IdentityController : AbpControllerBase
{
    protected IdentityController()
    {
        LocalizationResource = typeof(IdentityResource);
    }
}
