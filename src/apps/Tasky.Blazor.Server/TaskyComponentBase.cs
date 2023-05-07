using Tasky.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tasky.Blazor.Server;

public abstract class TaskyComponentBase : AbpComponentBase
{
    protected TaskyComponentBase()
    {
        LocalizationResource = typeof(TaskyResource);
    }
}
