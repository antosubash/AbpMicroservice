using Volo.Abp.Reflection;

namespace Tasky.IdentityService.Permissions;

public static class IdentityServicePermissions
{
    public const string GroupName = "IdentityService";

    public static class Users
    {
        public const string Default = GroupName + ".Users";
        public const string Create = Default + ".Create";
        public const string Update = Default + ".Update";
        public const string Delete = Default + ".Delete";
    }

    public static string[] GetAll()
    {
        return ReflectionHelper.GetPublicConstantsRecursively(typeof(IdentityServicePermissions));
    }
}