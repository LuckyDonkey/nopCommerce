using Nop.Core.Domain.ScheduleTasks;
using Nop.Plugin.Misc.WatermarkPro.Schedule;
using Nop.Services.Common;
using Nop.Services.Plugins;
using Nop.Services.ScheduleTasks;

namespace Nop.Plugin.Misc.WatermarkPro;
public class WatermarkProPlugin : BasePlugin, IMiscPlugin
{
    private readonly IScheduleTaskService _scheduleTaskService;

    public WatermarkProPlugin(IScheduleTaskService scheduleTaskService)
    {
        _scheduleTaskService = scheduleTaskService;
    }


    public async override Task InstallAsync()
    {
        // Define the task
        var task = new ScheduleTask
        {
            Name = "Your Custom Task",
            Seconds = 600, // Interval in seconds
            Type = typeof(YourCustomTask).AssemblyQualifiedName,
            Enabled = true,
            StopOnError = false
        };

        // Add the task to the database
        await _scheduleTaskService.InsertTaskAsync(task);

        await base.InstallAsync();
    }

    public async override Task UninstallAsync()
    {
        // Remove the task when uninstalling the plugin
        var task = await _scheduleTaskService.GetTaskByTypeAsync(typeof(YourCustomTask).AssemblyQualifiedName);
        if (task != null)
        {
            await _scheduleTaskService.DeleteTaskAsync(task);
        }

        await base.UninstallAsync();
    }
}
