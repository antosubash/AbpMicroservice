using System;
using System.Collections.Generic;
using System.Text;
using Tasky.WebApp.Localization;
using Volo.Abp.Application.Services;

namespace Tasky.WebApp;

/* Inherit your application services from this class.
 */
public abstract class WebAppAppService : ApplicationService
{
    protected WebAppAppService()
    {
        LocalizationResource = typeof(WebAppResource);
    }
}
