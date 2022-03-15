using Tasky.Administration.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.Administration;

public abstract class AdministrationController : AbpControllerBase
{
    protected AdministrationController()
    {
        LocalizationResource = typeof(AdministrationResource);
    }
}