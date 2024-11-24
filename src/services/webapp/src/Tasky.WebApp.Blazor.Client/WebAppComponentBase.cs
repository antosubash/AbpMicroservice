using Tasky.WebApp.Localization;
using Volo.Abp.AspNetCore.Components;

namespace Tasky.WebApp.Blazor.Client;

public abstract class WebAppComponentBase : AbpComponentBase
{
    protected WebAppComponentBase()
    {
        LocalizationResource = typeof(WebAppResource);
    }
}
