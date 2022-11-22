using System;
using System.Collections.Generic;
using System.Text;
using Tasky.Localization;
using Volo.Abp.Application.Services;

namespace Tasky;

/* Inherit your application services from this class.
 */
public abstract class TaskyAppService : ApplicationService
{
    protected TaskyAppService()
    {
        LocalizationResource = typeof(TaskyResource);
    }
}
