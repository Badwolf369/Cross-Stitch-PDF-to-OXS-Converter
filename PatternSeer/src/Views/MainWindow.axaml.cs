using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Platform.Storage;
using Avalonia.Input;
using Avalonia.Controls.Primitives;

namespace PatternSeer.Views;

/// <summary>
/// Primary View used when launched as a desktop application.
/// </summary>
public partial class MainWindow : Window
{
    /* #region Fields*/
    /* #endregion */

    /* #region Properties*/
    /* #endregion */

    /* #region Avalonia Properties*/
    /// <summary>
    /// Is/should the PDF file picker window open?
    /// </summary>
    public bool IsPdfPickerOpen
    {
        get => (bool)GetValue(IsPdfPickerOpenProperty);
        set => SetValue(IsPdfPickerOpenProperty, value);
    }
    /// <summary>
    /// Avalonia property to sync IsPdfPickerOpen with the DataContext.
    /// </summary>
    public static readonly AvaloniaProperty<bool>
        IsPdfPickerOpenProperty = AvaloniaProperty.
            Register<MainWindow, bool>(nameof(IsPdfPickerOpen));

    /// <summary>
    /// Path to the currently opened PDF file.
    /// </summary>
    public string PdfFilePath
    {
        get => (string)GetValue(PdfFilePathProperty);
        set => SetValue(PdfFilePathProperty, value);
    }
    /// <summary>
    /// Avalonia property to sync PdfFilePath with the DataContext.
    /// </summary>
    public static readonly AvaloniaProperty<string>
        PdfFilePathProperty = AvaloniaProperty.
            Register<MainWindow, string>(nameof(PdfFilePath));
    /* #endregion */

    /* #region Private Methods*/
    /* #endregion */

    /* #region Constructors*/
    /// <summary>
    /// Initializes a new instance of the <c>MainWindow</c> class.
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
    /* #endregion */

    /* #region Public Methods*/
    /// <summary>
    /// Event that is triggered when observable properties in the ViewModel are updated.
    /// </summary>
    /// <param name="sender">ViewModel that was updated.</param>
    /// <param name="e">Arguments related to the updated property.</param>
    public async void OnViewModelUpdate(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            case nameof(IsPdfPickerOpen):
                if (IsPdfPickerOpen)
                {
                    PdfFilePath = (await ViewUtils.OpenFilePickerAsync(
                        TopLevel.GetTopLevel(this),
                        "Open Cross Stitch Chart PDF",
                        false,
                        new[] { FilePickerFileTypes.Pdf }
                    ))[0];
                    IsPdfPickerOpen = false;
                }
                break;
        }
    }


    /// <summary>
    /// Event that is triggered when a key is pressed down.
    /// </summary>
    /// <param name="sender">Control that detected the keypress.</param>
    /// <param name="e">Arguments related to the key press.</param>
    public void OnKeyDown(object sender, KeyEventArgs e)
    {
        Console.WriteLine(e.Key.ToString() + "down");
        switch (e.Key)
        {
            // When the left or right CTRL key is pressed down,
            // disable scrolling and anable zooming
            case Key.LeftCtrl:
                PdfViewerScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                // ZoomEnabled = true;
                break;
            case Key.RightCtrl:
                PdfViewerScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                // ZoomEnabled = true;
                break;
        }
    }

    /// <summary>
    /// Event that is triggered when a keypress is released.
    /// </summary>
    /// <param name="sender">Control that detected the keypress release.</param>
    /// <param name="e">Arguments related to the keypress release.</param>
    public void OnKeyUp(object sender, KeyEventArgs e)
    {
        Console.WriteLine(e.Key.ToString() + "up");
        switch (e.Key)
        {
            // When the left or right CTRL key is released,
            // disable zooming and enable scrolling
            case Key.LeftCtrl:
                // ZoomEnabled = false;
                PdfViewerScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                break;
            case Key.RightCtrl:
                // ZoomEnabled = false;
                PdfViewerScroller.VerticalScrollBarVisibility = ScrollBarVisibility.Auto;
                break;
        }
    }
    /* #endregion */
}
