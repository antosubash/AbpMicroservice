using Volo.Abp.DependencyInjection;
using Volo.Abp.Ui.Branding;

namespace Tasky;

[Dependency(ReplaceServices = true)]
public class TaskyBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Tasky";
}