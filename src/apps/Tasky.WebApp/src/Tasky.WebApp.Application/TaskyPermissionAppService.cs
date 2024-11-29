using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.PermissionManagement;
using Volo.Abp.SimpleStateChecking;

namespace Tasky.WebApp
{
    [Dependency(ReplaceServices = true)]
    [ExposeServices(typeof(PermissionAppService), typeof(IPermissionAppService))]
    public class TaskyPermissionAppService(
        ILogger<TaskyPermissionAppService> logger,
        IOptions<PermissionManagementOptions> options,
        IPermissionDefinitionManager permissionDefinitionManager,
        IPermissionManager permissionManager,
        ISimpleStateCheckerManager<PermissionDefinition> simpleStateCheckerManager
        ) : PermissionAppService(permissionManager, permissionDefinitionManager, options, simpleStateCheckerManager), ITransientDependency
    {
        private readonly ILogger<TaskyPermissionAppService> _logger = logger;

        public override Task<GetPermissionListResultDto> GetAsync(string providerName, string providerKey)
        {
            _logger.LogInformation("Getting permissions for provider: {providerName}, providerKey: {providerKey}", providerName, providerKey);
            return base.GetAsync(providerName, providerKey);
        }

        public override Task UpdateAsync(string providerName, string providerKey, UpdatePermissionsDto input)
        {
            _logger.LogInformation("Updating permissions for provider: {providerName}, providerKey: {providerKey}", providerName, providerKey);
            return base.UpdateAsync(providerName, providerKey, input);
        }
    }
}
