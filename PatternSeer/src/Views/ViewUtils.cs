using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace PatternSeer.Views;

public class ViewUtils {
    /// <summary>
    /// Asynchronously opens the system's file picker, allowing only
    /// one PDF file to be picked.
    /// </summary>
    /// <param name="topLevel">TopLevel object of the window opening the file picker.</param>
    /// <returns>Path to the opened PDF file</returns>
    public static async Task<string> OpenFilePickerAsync(TopLevel topLevel)
    {
        Console.WriteLine("Opening file selection dialogue");
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = "Open cross stitch chart PDF",
                AllowMultiple = false,
                FileTypeFilter = new[] { FilePickerFileTypes.Pdf }
            });
        Console.WriteLine("Closing file selection dialogue");

        if (files.Count > 0)
        {
            return files[0].Path.ToString().Remove(0, 8);
        }
        else
        {
            return null;
        }
    }
}
