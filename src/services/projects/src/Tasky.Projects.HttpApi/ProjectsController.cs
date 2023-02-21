using Tasky.Projects.Localization;
using Volo.Abp.AspNetCore.Mvc;

namespace Tasky.Projects;

public abstract class ProjectsController : AbpControllerBase
{
    protected ProjectsController()
    {
        LocalizationResource = typeof(ProjectsResource);
    }
}