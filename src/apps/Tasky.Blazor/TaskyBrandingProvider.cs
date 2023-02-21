using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Tasky.Blazor;

[Dependency(ReplaceServices = true)]
public class TaskyBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Tasky";
}
