using Avalonia.Controls;
using Avalonia.Platform.Storage;

namespace PatternSeer.Views;

public class ViewUtils
{
    /// <summary>
    /// Asynchronously opens the system's file picker
    /// </summary>
    /// <param name="topLevel">TopLevel object of the window opening the file picker.</param>
    /// <param name="title">Title to display on file picker window.</param>
    /// <param name="allowMultiple"/>Allow the user to pick multiple files?</param>
    /// <param name="allowedFileTypes"/>List of allowed file types</param>
    /// <returns>List of path(s) to opened files</returns>
    /// <returns><c>null</c> if no file is opened</returns>
    public static async Task<List<string>> OpenFilePickerAsync(TopLevel topLevel, string title,
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
            if (allowMultiple)
            {
                List<string> paths = new List<string>();
                for (int i = 0; i < files.Count; i++)
                {
                    paths.Add(files[i].Path.ToString().Remove(0, 8));
                }
                return paths;
            }
            else
            {
                return new List<string> { files[0].Path.ToString().Remove(0, 8) };
            }
        }
        else
        {
            return null;
        }
    }
}
