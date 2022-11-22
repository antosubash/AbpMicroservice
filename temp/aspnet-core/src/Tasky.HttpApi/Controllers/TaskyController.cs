using Tasky.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.Controllers;

/* Inherit your controllers from this class.
 */
public abstract class TaskyController : AbpControllerBase
{
    protected TaskyController()
    {
        LocalizationResource = typeof(TaskyResource);
    }
}
