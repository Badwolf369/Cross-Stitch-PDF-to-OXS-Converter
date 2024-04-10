using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace PatternSeer.Views;

/// <summary>
/// Primary View used when launched as a desktop application.
/// </summary>
public partial class MainWindow : Window
{
    /// <summary>
    /// Initialize a new instance of the <c>MainWindow</c> class.
    /// </summary>
    public MainWindow()
    {
        InitializeComponent();
        this.AttachDevTools();
        this.Bind(IsPdfPickerOpenProperty, new Binding("IsPdfPickerOpen")
            { Mode = BindingMode.TwoWay });
        this.Bind(PdfFilePathProperty, new Binding("PdfFilePath")
            { Mode = BindingMode.TwoWay });
    }

    /// <summary>
    /// Event that is triggered when observable properties in the
    /// ViewModel are updated.
    /// </summary>
    /// <param name="sender">ViewModel that is updated.</param>
    /// <param name="e">Arguments related to the update event.</param>
    public async void OnViewModelUpdate(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IsPdfPickerOpen):
                if (IsPdfPickerOpen)
                {
                    PdfFilePath = await ViewUtils.OpenFilePickerAsync(
                        TopLevel.GetTopLevel(this)
                    );
                    IsPdfPickerOpen = false;
                }
                break;
        }
    }

    /* #region ViewModel-synced properties */
    /// <summary>
    /// Avalonia property to sync IsPdfPickerOpen with the view
    /// </summary>
    public static readonly AvaloniaProperty<bool>
        IsPdfPickerOpenProperty = AvaloniaProperty.
        Register<MainWindow, bool>(nameof(IsPdfPickerOpen));
    public bool IsPdfPickerOpen
    {
        get { return (bool)GetValue(IsPdfPickerOpenProperty); }
        set { SetValue(IsPdfPickerOpenProperty, value); }
    }
    /// <summary>
    /// Avalonia property to sync OpenedFile with the view
    /// </summary>
    public static readonly AvaloniaProperty<string>
        PdfFilePathProperty = AvaloniaProperty.
        Register<MainWindow, string>(nameof(PdfFilePath));
    /// <summary>
    /// Path to the currently opened file
    /// </summary>
    public string PdfFilePath
    {
        get { return (string)GetValue(PdfFilePathProperty); }
        set { SetValue(PdfFilePathProperty, value); }
    }
    /* #endregion ViewModel-synced properties */
}