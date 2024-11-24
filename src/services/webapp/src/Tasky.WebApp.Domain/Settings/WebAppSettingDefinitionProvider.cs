using Volo.Abp.Settings;

namespace Tasky.WebApp.Settings;

public class WebAppSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(WebAppSettings.MySetting1));
    }
}
