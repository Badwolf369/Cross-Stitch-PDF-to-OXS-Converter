using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace PatternSeer.Views;

public class ViewUtils {
    /// <summary>
    /// Asynchronously opens the system's file picker, allowing only
    /// one PDF file to be picked.
    /// </summary>
    /// <param name="topLevel">TopLevel object of the window opening the file picker.</param>
    /// <param name="title">Title to display on file picker window.</param>
    /// <param name="allowMultiple"/>Allow the user to pick multiple files?</param>
    /// <param name="allowedFileTypes"/>List of allowed file types</param>
    /// <returns>Path to the opened PDF file</returns>
    public static async Task<string> OpenFilePickerAsync(TopLevel topLevel, string title,
        bool allowMultiple, FilePickerFileType[] allowedFileTypes)
    {
        Console.WriteLine("Opening file selection dialogue");
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = title,
                AllowMultiple = allowMultiple,
                FileTypeFilter = allowedFileTypes
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
