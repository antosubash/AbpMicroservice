using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;

namespace Tasky.Administration;

public class TenantCreatedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private readonly ICurrentTenant currentTenant;
    private readonly IUnitOfWorkManager unitOfWorkManager;
    private readonly IPermissionDefinitionManager permissionDefinitionManager;
    private readonly IPermissionDataSeeder permissionDataSeeder;
    private readonly ILogger<TenantCreatedEventHandler> logger;

    public TenantCreatedEventHandler(
            ICurrentTenant currentTenant,
            IUnitOfWorkManager unitOfWorkManager,
            IPermissionDefinitionManager permissionDefinitionManager,
            IPermissionDataSeeder permissionDataSeeder,
            ILogger<TenantCreatedEventHandler> logger)
    {
        this.currentTenant = currentTenant;
        this.unitOfWorkManager = unitOfWorkManager;
        this.permissionDefinitionManager = permissionDefinitionManager;
        this.permissionDataSeeder = permissionDataSeeder;
        this.logger = logger;
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
        logger.LogInformation($"Seeding ${tenantId}");
        using (currentTenant.Change(tenantId))
        {
            var abpUnitOfWorkOptions = new AbpUnitOfWorkOptions { IsTransactional = true };
            using var uow = unitOfWorkManager.Begin(abpUnitOfWorkOptions, requiresNew: true);
            var multiTenancySide = tenantId is null
                ? MultiTenancySides.Host
                : MultiTenancySides.Tenant;

            var permissionNames = permissionDefinitionManager
                .GetPermissions()
                .Where(p => p.MultiTenancySide.HasFlag(multiTenancySide))
                .Where(p => !p.Providers.Any() || p.Providers.Contains(RolePermissionValueProvider.ProviderName))
                .Select(p => p.Name)
                .ToArray();

            await permissionDataSeeder.SeedAsync(
                RolePermissionValueProvider.ProviderName,
                "admin",
                permissionNames,
                tenantId
            );

            await uow.CompleteAsync();
        }
    }
}