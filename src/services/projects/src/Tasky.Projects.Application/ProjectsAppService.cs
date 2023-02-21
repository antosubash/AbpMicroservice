using Tasky.Projects.Localization;
using Volo.Abp.Application.Services;

namespace Tasky.Projects;

public abstract class ProjectsAppService : ApplicationService
{
    protected ProjectsAppService()
    {
        LocalizationResource = typeof(ProjectsResource);
        ObjectMapperContext = typeof(ProjectsApplicationModule);
    }
}