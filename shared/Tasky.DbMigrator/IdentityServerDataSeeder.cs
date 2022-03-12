using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Volo.Abp.Identity;
using Volo.Abp.IdentityServer.ApiResources;
using Volo.Abp.IdentityServer.ApiScopes;
using Volo.Abp.IdentityServer.Clients;
using Volo.Abp.IdentityServer.IdentityResources;
using Volo.Abp.MultiTenancy;
using Volo.Abp.PermissionManagement;
using Volo.Abp.Uow;
using ApiResource = Volo.Abp.IdentityServer.ApiResources.ApiResource;
using ApiScope = Volo.Abp.IdentityServer.ApiScopes.ApiScope;
using Client = Volo.Abp.IdentityServer.Clients.Client;

namespace Tasky.DbMigrator
{
 public class IdentityServerDataSeeder : ITransientDependency
    {
        private readonly IApiResourceRepository _apiResourceRepository;
        private readonly IApiScopeRepository _apiScopeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly IIdentityResourceDataSeeder _identityResourceDataSeeder;
        private readonly IGuidGenerator _guidGenerator;
        private readonly IPermissionDataSeeder _permissionDataSeeder;
        private readonly IConfiguration _configuration;
        private readonly ICurrentTenant _currentTenant;

        public IdentityServerDataSeeder(
            IClientRepository clientRepository,
            IApiResourceRepository apiResourceRepository,
            IApiScopeRepository apiScopeRepository,
            IIdentityResourceDataSeeder identityResourceDataSeeder,
            IGuidGenerator guidGenerator,
            IPermissionDataSeeder permissionDataSeeder,
            IConfiguration configuration,
            ICurrentTenant currentTenant)
        {
            _clientRepository = clientRepository;
            _apiResourceRepository = apiResourceRepository;
            _apiScopeRepository = apiScopeRepository;
            _identityResourceDataSeeder = identityResourceDataSeeder;
            _guidGenerator = guidGenerator;
            _permissionDataSeeder = permissionDataSeeder;
            _configuration = configuration;
            _currentTenant = currentTenant;
        }

        [UnitOfWork]
        public virtual async Task SeedAsync()
        {
            using (_currentTenant.Change(null))
            {
                await _identityResourceDataSeeder.CreateStandardResourcesAsync();
                await CreateApiResourcesAsync();
                await CreateApiScopesAsync();
                await CreateSwaggerClientsAsync();
                await CreateClientsAsync();
            }
        }

        private async Task CreateApiResourcesAsync()
        {
            var commonApiUserClaims = new[]
            {
                "email",
                "email_verified",
                "name",
                "phone_number",
                "phone_number_verified",
                "role"
            };

            await CreateApiResourceAsync("AuthServer", commonApiUserClaims);
            await CreateApiResourceAsync("IdentityService", commonApiUserClaims);
            await CreateApiResourceAsync("AdministrationService", commonApiUserClaims);
            await CreateApiResourceAsync("SaasService", commonApiUserClaims);
        }

        private async Task CreateApiScopesAsync()
        {
            await CreateApiScopeAsync("AuthServer");
            await CreateApiScopeAsync("IdentityService");
            await CreateApiScopeAsync("AdministrationService");
            await CreateApiScopeAsync("SaasService");

        }

        private async Task CreateSwaggerClientsAsync()
        {
            await CreateSwaggerClientAsync("Administration", new []{ "AuthServer", "AdministrationService" });
            await CreateSwaggerClientAsync("IdentityService", new []{ "AuthServer", "IdentityService"});
            await CreateSwaggerClientAsync("SaaS", new []{ "AuthServer", "SaasService" });
        }

        private async Task CreateSwaggerClientAsync(string name, string[] scopes = null)
        {
            var commonScopes = new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address"
            };
            scopes ??= new[] {name};

            // Swagger Client
            var swaggerClientId = $"{name}_Swagger";
            if (!swaggerClientId.IsNullOrWhiteSpace())
            {
                string sectionName = $"IdentityServerClients:{name}:RootUrl";
                var swaggerRootUrls = _configuration.GetSection(sectionName).Get<string[]>().Select(x => x.TrimEnd('/'));

                await CreateClientAsync(
                    name: swaggerClientId,
                    scopes: commonScopes.Union(scopes),
                    grantTypes: new[] { "authorization_code" },
                    secret: "1q2w3e*".Sha256(),
                    requireClientSecret: false,
                    redirectUris: swaggerRootUrls.Select( x => $"{x}/swagger/oauth2-redirect.html" ),
                    corsOrigins: swaggerRootUrls.Select( x => x.RemovePostFix("/") )
                );
            }
        }

        private async Task<ApiResource> CreateApiResourceAsync(string name, IEnumerable<string> claims)
        {
            var apiResource = await _apiResourceRepository.FindByNameAsync(name);
            if (apiResource == null)
            {
                apiResource = await _apiResourceRepository.InsertAsync(
                    new ApiResource(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            foreach (var claim in claims)
            {
                if (apiResource.FindClaim(claim) == null)
                {
                    apiResource.AddUserClaim(claim);
                }
            }

            return await _apiResourceRepository.UpdateAsync(apiResource);
        }

        private async Task<ApiScope> CreateApiScopeAsync(string name)
        {
            var apiScope = await _apiScopeRepository.FindByNameAsync(name);
            if (apiScope == null)
            {
                apiScope = await _apiScopeRepository.InsertAsync(
                    new ApiScope(
                        _guidGenerator.Create(),
                        name,
                        name + " API"
                    ),
                    autoSave: true
                );
            }

            return apiScope;
        }

        private async Task CreateClientsAsync()
        {
            var commonScopes = new[]
            {
                "email",
                "openid",
                "profile",
                "role",
                "phone",
                "address"
            };

            //Web Client
            var webClientRootUrl =
                _configuration["IdentityServerClients:ZW_Web:RootUrl"].EnsureEndsWith('/');
            await CreateClientAsync(
                name: "ZW_Web",
                //Add references to new microservice services
                scopes: commonScopes.Union(new[]
                {
                    "AuthServer",
                    "IdentityService",
                    "AdministrationService",
                    "SaasService",
                    "ProductService",
                    "LeaveMgmtSvc"
                }),
                grantTypes: new[] {"hybrid"},
                secret: "1q2w3e*".Sha256(),
                redirectUris: new [] { $"{webClientRootUrl}signin-oidc" },
                postLogoutRedirectUris: new [] { $"{webClientRootUrl}signout-callback-oidc" },
                frontChannelLogoutUri: $"{webClientRootUrl}Account/FrontChannelLogout",
                corsOrigins: new[] {webClientRootUrl.RemovePostFix("/")}
            );

            //Blazor Client
            var blazorClientRootUrl =
                _configuration["IdentityServerClients:ZW_Blazor:RootUrl"].EnsureEndsWith('/');
            await CreateClientAsync(
                name: "ZW_Blazor",
                //Add references to new microservice services
                scopes: commonScopes.Union(new[]
                {
                    "AuthServer",
                    "IdentityService",
                    "AdministrationService",
                    "SaasService",
                    "ProductService",
                    "LeaveMgmtSvc"
                }),
                grantTypes: new[] {"authorization_code"},
                secret: "1q2w3e*".Sha256(),
                requireClientSecret: false,
                redirectUris: new [] { $"{blazorClientRootUrl}authentication/login-callback" },
                postLogoutRedirectUris: new [] { $"{blazorClientRootUrl}authentication/logout-callback" },
                corsOrigins: new[] {blazorClientRootUrl.RemovePostFix("/")}
            );

            //Blazr Server Client
            var blazorServerClientRootUrl =
                _configuration["IdentityServerClients:ZW_BlazorServer:RootUrl"].EnsureEndsWith('/');
            await CreateClientAsync(
                name: "ZW_BlazorServer",
                //Add references to new microservice services
                scopes: commonScopes.Union(new[]
                {
                    "AuthServer",
                    "IdentityService",
                    "AdministrationService",
                    "SaasService",
                    "ProductService",
                    "LeaveMgmtSvc"
                }),
                grantTypes: new[] {"hybrid"},
                secret: "1q2w3e*".Sha256(),
                redirectUris: new [] { $"{blazorServerClientRootUrl}signin-oidc" },
                postLogoutRedirectUris: new [] {$"{blazorServerClientRootUrl}signout-callback-oidc" },
                frontChannelLogoutUri: $"{blazorServerClientRootUrl}Account/FrontChannelLogout",
                corsOrigins: new[] {blazorServerClientRootUrl.RemovePostFix("/")}
            );

            //Public Web Client
            var publicWebClientRootUrl = _configuration["IdentityServerClients:ZW_PublicWeb:RootUrl"]
                .EnsureEndsWith('/');
            await CreateClientAsync(
                name: "ZW_PublicWeb",
                //Add references to new microservice services
                scopes: commonScopes.Union(new[]
                {
                    "AdministrationService",
                    "ProductService",
                    "LeaveMgmtSvc"
                }),
                grantTypes: new[] {"hybrid"},
                secret: "1q2w3e*".Sha256(),
                redirectUris: new []{ $"{publicWebClientRootUrl}signin-oidc" },
                postLogoutRedirectUris: new [] { $"{publicWebClientRootUrl}signout-callback-oidc" },
                frontChannelLogoutUri: $"{publicWebClientRootUrl}Account/FrontChannelLogout",
                corsOrigins: new[] { publicWebClientRootUrl.RemovePostFix("/") }
            );

            //Angular Client
            var angularClientRootUrl =
                _configuration["IdentityServerClients:ZW_Angular:RootUrl"].TrimEnd('/');
            await CreateClientAsync(
                name: "ZW_Angular",
                //Add references to new microservice services
                scopes: commonScopes.Union(new[]
                {
                    "AuthServer",
                    "IdentityService",
                    "AdministrationService",
                    "SaasService",
                    "ProductService",
                    "LeaveMgmtSvc"
                }),
                grantTypes: new[] {"authorization_code", "LinkLogin"},
                secret: "1q2w3e*".Sha256(),
                requireClientSecret: false,
                redirectUris: new[] { $"{angularClientRootUrl}" },
                postLogoutRedirectUris: new [] { $"{angularClientRootUrl}" },
                corsOrigins: new[] { angularClientRootUrl }
            );

            //Administration Service Client
            await CreateClientAsync(
                name: "ZW_AdministrationService",
                scopes: commonScopes.Union(new[]
                {
                    "IdentityService"
                }),
                grantTypes: new[] {"client_credentials"},
                secret: "1q2w3e*".Sha256(),
                permissions: new[] {IdentityPermissions.Users.Default}
            );
        }

        private async Task<Client> CreateClientAsync(
            string name,
            IEnumerable<string> scopes,
            IEnumerable<string> grantTypes,
            string secret = null,
            IEnumerable<string> redirectUris = null,
            IEnumerable<string> postLogoutRedirectUris = null,
            string frontChannelLogoutUri = null,
            bool requireClientSecret = true,
            bool requirePkce = false,
            IEnumerable<string> permissions = null,
            IEnumerable<string> corsOrigins = null)
        {
            var client = await _clientRepository.FindByClientIdAsync(name);
            if (client == null)
            {
                client = await _clientRepository.InsertAsync(
                    new Client(
                        _guidGenerator.Create(),
                        name
                    )
                    {
                        ClientName = name,
                        ProtocolType = "oidc",
                        Description = name,
                        AlwaysIncludeUserClaimsInIdToken = true,
                        AllowOfflineAccess = true,
                        AbsoluteRefreshTokenLifetime = 31536000, //365 days
                        AccessTokenLifetime = 31536000, //365 days
                        AuthorizationCodeLifetime = 300,
                        IdentityTokenLifetime = 300,
                        RequireConsent = false,
                        FrontChannelLogoutUri = frontChannelLogoutUri,
                        RequireClientSecret = requireClientSecret,
                        RequirePkce = requirePkce
                    },
                    autoSave: true
                );
            }

            foreach (var scope in scopes)
            {
                if (client.FindScope(scope) == null)
                {
                    client.AddScope(scope);
                }
            }

            foreach (var grantType in grantTypes)
            {
                if (client.FindGrantType(grantType) == null)
                {
                    client.AddGrantType(grantType);
                }
            }

            if (!secret.IsNullOrEmpty())
            {
                if (client.FindSecret(secret) == null)
                {
                    client.AddSecret(secret);
                }
            }

            foreach (var redirectUrl in redirectUris)
            {
                if (client.FindRedirectUri(redirectUrl) == null)
                {
                    client.AddRedirectUri(redirectUrl);
                }
            }

            foreach (var postLogoutRedirectUri in postLogoutRedirectUris)
            {
                if (client.FindPostLogoutRedirectUri(postLogoutRedirectUri) == null)
                {
                    client.AddPostLogoutRedirectUri(postLogoutRedirectUri);
                }
            }
            
            if (permissions != null)
            {
                await _permissionDataSeeder.SeedAsync(
                    ClientPermissionValueProvider.ProviderName,
                    name,
                    permissions,
                    null
                );
            }

            if (corsOrigins != null)
            {
                foreach (var origin in corsOrigins)
                {
                    if (!origin.IsNullOrWhiteSpace() && client.FindCorsOrigin(origin) == null)
                    {
                        client.AddCorsOrigin(origin);
                    }
                }
            }

            return await _clientRepository.UpdateAsync(client);
        }
    }

}