using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Tasky.Administration.EventHandler;

public class TenantCreatedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly ILogger<TenantCreatedEventHandler> _logger;
    private readonly IPermissionDataSeeder _permissionDataSeeder;
    private readonly IPermissionDefinitionManager _permissionDefinitionManager;
    private readonly IUnitOfWorkManager _unitOfWorkManager;

    public TenantCreatedEventHandler(
        ICurrentTenant currentTenant,
        IUnitOfWorkManager unitOfWorkManager,
        IPermissionDefinitionManager permissionDefinitionManager,
        IPermissionDataSeeder permissionDataSeeder,
        ILogger<TenantCreatedEventHandler> logger)
    {
        _currentTenant = currentTenant;
        _unitOfWorkManager = unitOfWorkManager;
        _permissionDefinitionManager = permissionDefinitionManager;
        _permissionDataSeeder = permissionDataSeeder;
        _logger = logger;
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            await SeedDataAsync(eventData.Id);
        }
        catch (Exception ex)
        {
            await HandleErrorTenantCreatedAsync(eventData, ex);
        }
    }

    private Task HandleErrorTenantCreatedAsync(TenantCreatedEto eventData, Exception ex)
    {
        throw new NotImplementedException();
    }

    private async Task SeedDataAsync(Guid? tenantId)
    {
        _logger.LogInformation($"Seeding ${tenantId}");
        using (_currentTenant.Change(tenantId))
        {
            var abpUnitOfWorkOptions = new AbpUnitOfWorkOptions {IsTransactional = true};
            using var uow = _unitOfWorkManager.Begin(abpUnitOfWorkOptions, true);
            var multiTenancySide = tenantId is null
                ? MultiTenancySides.Host
                : MultiTenancySides.Tenant;

            var permissionNames = _permissionDefinitionManager
                .GetPermissions()
                .Where(p => p.MultiTenancySide.HasFlag(multiTenancySide))
                .Where(p => !p.Providers.Any() || p.Providers.Contains(RolePermissionValueProvider.ProviderName))
                .Select(p => p.Name)
                .ToArray();

            await _permissionDataSeeder.SeedAsync(
                RolePermissionValueProvider.ProviderName,
                "admin",
                permissionNames,
                tenantId
            );

            await uow.CompleteAsync();
        }
    }
}