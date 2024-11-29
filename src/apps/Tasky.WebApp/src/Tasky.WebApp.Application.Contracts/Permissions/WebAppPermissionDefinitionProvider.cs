using Tasky.WebApp.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace Tasky.WebApp.Permissions;

public class WebAppPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(WebAppPermissions.GroupName);

        //Define your own permissions here. Example:
        myGroup.AddPermission(WebAppPermissions.MyPermission1, L("Permission:MyPermission1"));
    }

    private static LocalizableString L(string name) => LocalizableString.Create<WebAppResource>(name);
}
