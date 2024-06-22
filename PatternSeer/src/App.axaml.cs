using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PatternSeer.Views;
using PatternSeer.ViewModels;

namespace PatternSeer;

/// <summary>
/// Main wrapper for an Avalonia app.
/// </summary>
public partial class App : Application {
    /// <summary>
    /// Initializes the Avalonia app.
    /// </summary>
    public override void Initialize() {
        AvaloniaXamlLoader.Load(this);
    }

    /// <summary>
    /// Run after the initial creation of the Avalonia app is finished.
    /// </summary>
    public override void OnFrameworkInitializationCompleted() {
        MainViewModel viewmodel = new MainViewModel();

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop) {
            MainWindow window = new MainWindow
            {
                DataContext = viewmodel
            };
            viewmodel.PropertyChanged += window.OnViewModelUpdate;
            viewmodel.PropertyChanged += viewmodel.OnViewModelUpdate;
            desktop.MainWindow = window;
        }

        base.OnFrameworkInitializationCompleted();
    }
}