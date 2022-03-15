namespace Tasky.DbMigrator;

public class ServiceClient
{
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string[] RootUrls { get; set; }
    public string[] Scopes { get; set; }
    public string[] GrantTypes { get; set; }
    public string[] RedirectUris { get; set; }
    public string[] PostLogoutRedirectUris { get; set; }
    public string[] AllowedCorsOrigins { get; set; }
}