using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Configuration;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.WatermarkPro.Services;
using Nop.Services.Media;

namespace Nop.Plugin.Misc.WatermarkPro.Infrastructure;
public class NopStartup : INopStartup
{
    public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.Get<AzureBlobConfig>().Enabled)
            services.AddScoped<IPictureService, MiscWatermarkProAzurePictureService>();
        else
            services.AddScoped<IPictureService, MiscWatermarkProPictureService>();

        services.AddScoped<FontProvider>();
    }

    public void Configure(IApplicationBuilder application)
    {
    }

    public int Order => 3000;
}