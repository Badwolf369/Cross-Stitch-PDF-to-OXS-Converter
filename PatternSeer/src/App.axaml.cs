using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PatternSeer.Views;
using PatternSeer.ViewModels;

namespace PatternSeer;

/// <summary>
/// Main object containing the Avalonia program.
/// </summary>
public partial class App : Application
{
    /// <summary>
    /// Code that runs on program initialization.
    /// </summary>
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Code that runs after the program's initialization is finished.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        MainViewModel viewmodel = new MainViewModel();

        // Start up a desktop application
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            MainWindow window = new MainWindow
            {
                DataContext = viewmodel
            };
            viewmodel.PropertyChanged += window.OnViewModelUpdate;
            viewmodel.PropertyChanged += viewmodel.OnViewModelUpdate;
            window.KeyDown += window.OnKeyDown;
            window.KeyUp += window.OnKeyUp;
            desktop.MainWindow = window;
        }

        base.OnFrameworkInitializationCompleted();
    }
}