using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using JishoTangoAssistant.Domain.Common.Data;
using JishoTangoAssistant.Domain.Models.Common.Data;
using Newtonsoft.Json;
using JishoTangoAssistantWindowView = JishoTangoAssistant.Presentation.UI.Views.JishoTangoAssistantWindowView;

namespace JishoTangoAssistant.Presentation.UI.Utils;

public static class FilePicker {
    public static async Task<FileInfo<T>?> LoadAsync<T>(string? title = null, IReadOnlyList<FilePickerFileType>? options = null, string? startLocationPath = null)
    {
        var loadedFileInfo = await LoadAsync(title, options);

        if (loadedFileInfo == null)
            return null;
        
        var fileContent = loadedFileInfo.Content;
        var content = JsonConvert.DeserializeObject<T>(fileContent);
        return content != null ? new FileInfo<T>(content, loadedFileInfo.FilePath) : null;
    }
    
    public static async Task<FileInfo<string>?> LoadAsync(string? title = null, IReadOnlyList<FilePickerFileType>? options = null, string? startLocationPath = null)
    {
        var mainWindow = App.GetMainWindow();
    
        var topLevel = TopLevel.GetTopLevel(mainWindow);
        
        if (topLevel == null)
            return null;
            
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            FileTypeFilter = options,
            SuggestedStartLocation = startLocationPath != null ? await topLevel.StorageProvider.TryGetFolderFromPathAsync(new Uri(startLocationPath)) : null
        });

        if (files.Count < 1) 
            return null;

        var filePath = Uri.UnescapeDataString(files[0].Path.AbsolutePath);
        await using var stream = await files[0].OpenReadAsync();
        using var streamReader = new StreamReader(stream);
        var fileContent = await streamReader.ReadToEndAsync();
        
        (App.GetMainWindow() as JishoTangoAssistantWindowView)?.FocusSelectedContentControlView();
        
        return new FileInfo<string>(fileContent, filePath);
    }
    
    // return null, if saving was not successful
    public static async Task<string?> GetFilePathForSaveAsync(string? title = null, IReadOnlyList<FilePickerFileType>? options = null, string? startLocationPath = null, string? suggestedFileName = null)
    {
        var mainWindow = App.GetMainWindow();
    
        var topLevel = TopLevel.GetTopLevel(mainWindow);
        
        if (topLevel == null)
            return null;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            FileTypeChoices = options,
            SuggestedStartLocation = startLocationPath != null ? await topLevel.StorageProvider.TryGetFolderFromPathAsync(new Uri(startLocationPath)) : null,
            SuggestedFileName = suggestedFileName
        });

        if (file == null) 
            return null;

        var filePath = Uri.UnescapeDataString(file.Path.AbsolutePath);

        (App.GetMainWindow() as JishoTangoAssistantWindowView)?.FocusSelectedContentControlView();
        
        return filePath;
    }
}