using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tasky;

[Dependency(ReplaceServices = true)]
public class TaskyBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Tasky";
}
