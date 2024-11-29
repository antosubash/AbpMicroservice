using Tasky.WebApp.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.WebApp.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class WebAppController : AbpControllerBase
{
    protected WebAppController()
    {
        LocalizationResource = typeof(WebAppResource);
    }
}
