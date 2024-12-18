using FluentValidation;
using Nop.Plugin.Misc.WatermarkPro.Models;
using Nop.Services.Localization;
using Nop.Web.Framework.Validators;

namespace Nop.Plugin.Misc.WatermarkPro.Infrastructure;
public class ConfigurationModelValidator : BaseNopValidator<ConfigurationModel>
{
    public ConfigurationModelValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.TextSettings).SetValidator(new CommonWatermarkSettingsValidator(localizationService));
        RuleFor(x => x.PictureSettings).SetValidator(new CommonWatermarkSettingsValidator(localizationService));
    }
}

public class CommonWatermarkSettingsValidator : AbstractValidator<CommonWatermarkSettings>
{
    public CommonWatermarkSettingsValidator(ILocalizationService localizationService)
    {
        RuleFor(x => x.Opacity)
            .InclusiveBetween(0, 1)
            .WithMessage(string.Format(
                localizationService.GetResourceAsync("Plugins.Misc.Watermark.WatermarkOpacityErrorMessage").Result,
                0, 1));

        RuleFor(x => x.Size)
            .InclusiveBetween(0, 100)
            .WithMessage(string.Format(
                localizationService.GetResourceAsync("Plugins.Misc.Watermark.SizeErrorMessage").Result, 0, 100));
    }
}
