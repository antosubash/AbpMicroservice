using Tasky.Identity.Localization;
using Volo.Abp.Application.Services;

namespace Tasky.Identity;

public abstract class IdentityAppService : ApplicationService
{
    protected IdentityAppService()
    {
        LocalizationResource = typeof(IdentityResource);
        ObjectMapperContext = typeof(IdentityApplicationModule);
    }
}
