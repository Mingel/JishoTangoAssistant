using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using JishoTangoAssistant.Common.Data;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Common.Utils;

public static class FilePicker {
    public static async Task<FileInfo<IEnumerable<T>>?> LoadAsync<T>(string? title = null, IReadOnlyList<FilePickerFileType>? options = null, string? startLocationPath = null)
    {
        var loadedFileInfo = await LoadAsync(title, options);

        if (loadedFileInfo == null)
            return null;
        
        var fileContent = loadedFileInfo.Content;
        var content = JsonConvert.DeserializeObject<T[]>(fileContent);
        return content != null ? new FileInfo<IEnumerable<T>>(content, loadedFileInfo.FilePath) : null;
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
        return new FileInfo<string>(fileContent, filePath);
    }
    
    // return null, if saving was not successful
    public static async Task<FileInfo<IEnumerable<T>>?> SaveAsync<T>(IEnumerable<T> items, string? title = null, IReadOnlyList<FilePickerFileType>? options = null, string? startLocationPath = null, string? suggestedFileName = null)
    {
        var json = JsonConvert.SerializeObject(items, Formatting.Indented);
        var savedFileInfo = await SaveAsync(json, title, options, startLocationPath, suggestedFileName);
        return savedFileInfo != null ? new FileInfo<IEnumerable<T>>(items, savedFileInfo.FilePath) : null;
    }
    
    // return null, if saving was not successful
    public static async Task<FileInfo<string>?> SaveAsync(string content, string? title = null, IReadOnlyList<FilePickerFileType>? options = null, string? startLocationPath = null, string? suggestedFileName = null)
    {
        ArgumentNullException.ThrowIfNull(content);
        
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
        await using var stream = await file.OpenWriteAsync();
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
        await streamWriter.WriteAsync(content);
        return new FileInfo<string>(content, filePath);
    }
}