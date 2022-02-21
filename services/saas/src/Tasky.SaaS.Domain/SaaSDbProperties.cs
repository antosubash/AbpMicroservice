namespace Tasky.SaaS;

public static class SaaSDbProperties
{
    public static string DbTablePrefix { get; set; } = "SaaS";

    public static string DbSchema { get; set; } = null;

    public const string ConnectionStringName = "SaaS";
}
