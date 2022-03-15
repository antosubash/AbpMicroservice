using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Volo.Abp.DependencyInjection;
using Volo.Abp.EventBus.Distributed;
using Volo.Abp.MultiTenancy;
using Volo.Abp.Identity;
using System.Collections.Generic;

namespace Tasky.IdentityService.EventHandler;

public class TenantCreatedEventHandler : IDistributedEventHandler<TenantCreatedEto>, ITransientDependency
{
    private readonly ICurrentTenant _currentTenant;
    private readonly ILogger<TenantCreatedEventHandler> _logger;
    private readonly IIdentityDataSeeder _identityDataSeeder;
    public TenantCreatedEventHandler(
        ICurrentTenant currentTenant,
        IIdentityDataSeeder identityDataSeeder,
        ILogger<TenantCreatedEventHandler> logger)
    {
        _currentTenant = currentTenant;
        _identityDataSeeder = identityDataSeeder;
        _logger = logger;
    }

    public async Task HandleEventAsync(TenantCreatedEto eventData)
    {
        try
        {
            using (_currentTenant.Change(eventData.Id))
            {
                
                _logger.LogInformation($"Creating admin user for tenant {eventData.Id}...");
                await _identityDataSeeder.SeedAsync(
                    eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminEmailPropertyName) ?? "admin@antosubash.com",
                    eventData.Properties.GetOrDefault(IdentityDataSeedContributor.AdminPasswordPropertyName) ?? "1q2w3E*",
                    eventData.Id
                );
            }
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
}