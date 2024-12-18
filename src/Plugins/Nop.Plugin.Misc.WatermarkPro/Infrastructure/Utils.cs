using SkiaSharp;

namespace Nop.Plugin.Misc.WatermarkPro.Infrastructure;
public static class Utils
{
    public static string ToRgb24Hex(this SKColor color) => color.ToString()[3..];
}
