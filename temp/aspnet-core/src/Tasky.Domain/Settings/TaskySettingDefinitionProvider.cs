using Volo.Abp.Settings;

namespace Tasky.Settings;

public class TaskySettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(TaskySettings.MySetting1));
    }
}
