using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Emgu.CV;
using PatternSeer.Models;

namespace PatternSeer.ViewModels;

/// <summary>
/// Primary ViewModel associated with the View <c>MainView</c>
/// </summary>
public partial class MainViewModel : ObservableObject, INotifyPropertyChanged
{
    /* #region Fields */
    private Chart _chart;
    /// <summary>
    /// Signal to allow import and open commands to pause and wait until
    /// the file picker closes.
    /// </summary>
    private SemaphoreSlim _filePickerSemaphore;
    /* #endregion Fields */

    /* #region Properties */
    /* #endregion Properties */

    /* #region Observable Properties */
    /// <summary>
    /// Is/should the pdf file picker currently be open?
    /// </summary>
    [ObservableProperty]
    private bool _isPdfPickerOpen;
    /// <summary>
    /// Path to the PDF file currently opened by the ViewModel.
    /// </summary>
    [ObservableProperty]
    private string _pdfFilePath;
    /// <summary>
    /// List of the pages in the currently opened PDF.
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Mat> _pdfPages;
    /// <summary>
    /// Zoom level for the PDF viewer.
    /// </summary>
    [ObservableProperty]
    private string _pdfZoomLevel = "Zoom: 100%";
    /// <summary>
    /// Which page(s) of the PDF is currently visible in the PDF viewer?
    /// </summary>
    [ObservableProperty]
    private string _visiblePdfPages = "Page 0";
    /* #endregion Observable Properties*/

    /* #region Constructors */
    /// <summary>
    /// Initializes a new instance of the <c>MainWindowViewModel</c> class.
    /// </summary>
    public MainViewModel()
    {
        IsPdfPickerOpen = false;
        _chart = new Chart();
    }
    /* #endregion Constructors */

    /* #region Private Methods */
    /* #endregion Private Methods */

    /* #region Public Methods */
    /// <summary>
    /// Runs when an observable property in the ViewModel is updated.
    /// </summary>
    /// <param name="sender">ViewModel being updated.</param>
    /// <param name="e">Arguments related to the updated property.</param>
    public void OnViewModelUpdate(object sender, PropertyChangedEventArgs e)
    {
        switch (e.PropertyName)
        {
            // When IsPdfPickerOpen is updated to false, release a semaphore
            // to everything waiting for the pdf picker to close
            case (nameof(IsPdfPickerOpen)):
                if (!IsPdfPickerOpen) _filePickerSemaphore.Release();
                break;
        }
    }
    /* #endregion Public Methods */

    /* #region ICommands */
    /// <summary>
    /// Open a file picker then print the path to the picked file.
    /// </summary>
    [RelayCommand]
    private async void ImportFromPdf()
    {
        _filePickerSemaphore = new SemaphoreSlim(0, 1);
        IsPdfPickerOpen = true;
        await _filePickerSemaphore.WaitAsync();

        if (PdfFilePath is not null) {
            Debug.WriteLine($"Picked {PdfFilePath}");
            _chart.ImportPdf(PdfFilePath);
            PdfPages = new ObservableCollection<Mat>(_chart.PdfPages);
        } else {
            Debug.WriteLine("No file was picked");
        }
    }
    /* #endregion ICommands */
}

