using Tasky.Administration.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tasky.Administration.Permissions;

public class AdministrationPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(AdministrationPermissions.GroupName, L("Permission:Administration"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<AdministrationResource>(name);
    }
}