using Tasky.Localization;
using Volo.Abp.AspNetCore.Mvc.UI.RazorPages;

namespace Tasky.Web.Pages;

public abstract class TaskyPageModel : AbpPageModel
{
    protected TaskyPageModel()
    {
        LocalizationResourceType = typeof(TaskyResource);
    }
}
