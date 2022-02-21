using Volo.Abp.Ui.Branding;
using Volo.Abp.DependencyInjection;

namespace Tasky.Web;

[Dependency(ReplaceServices = true)]
public class TaskyBrandingProvider : DefaultBrandingProvider
{
    public override string AppName => "Tasky";
}
