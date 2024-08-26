using Avalonia;
using Emgu.CV;
using Emgu.CV.CvEnum;
using SkiaSharp;
using Avalonia.ReactiveUI;
using Emgu.CV.Structure;

namespace PatternSeer;

/// <summary>
/// Main entry point for the program.
/// </summary>
class Program
{
    /// <summary>
    /// Configures options for the creation of an Avalonia app.
    /// </summary>
    /// <returns>Configured Avalonia AppBuilder which
    /// can be used to start the Avalonia app.</returns>
    public static AppBuilder BuildAvaloniaApp() =>
        AppBuilder.Configure<App>()
        .UsePlatformDetect()
        .UseReactiveUI();

    /// <summary>
    /// Takes command-line arguments and starts the program.
    /// </summary>
    /// <param name="args">User's command-line input.</param>
    static void Main(string[] args)
    {
        BuildAvaloniaApp().StartWithClassicDesktopLifetime(args);
    }
}