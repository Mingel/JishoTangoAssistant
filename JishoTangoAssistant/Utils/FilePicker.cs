using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Utils;

public static class FilePicker {
    public static async Task<IEnumerable<T>?> LoadAsync<T>(string? title = null, IReadOnlyList<FilePickerFileType>? options = null)
    {
        var fileContent = await LoadAsync(title, options);
        return fileContent != null ? JsonConvert.DeserializeObject<T[]>(fileContent) : null;
    }
    
    public static async Task<string?> LoadAsync(string? title = null, IReadOnlyList<FilePickerFileType>? options = null)
    {
        var mainWindow = App.GetMainWindow();
    
        var topLevel = TopLevel.GetTopLevel(mainWindow);
        
        if (topLevel == null)
            return null;
            
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            FileTypeFilter = options
        });

        if (files.Count < 1) 
            return null;
        
        await using var stream = await files[0].OpenReadAsync();
        using var streamReader = new StreamReader(stream);
        var fileContent = await streamReader.ReadToEndAsync();
        return fileContent;
    }
    
    // return true, if saving was successful
    public static async Task<bool> SaveAsync<T>(IEnumerable<T> items, string? title = null, IReadOnlyList<FilePickerFileType>? options = null)
    {
        var json = JsonConvert.SerializeObject(items, Formatting.Indented);
        return await SaveAsync(json, title, options); 
    }
    
    // return true, if saving was successful
    public static async Task<bool> SaveAsync(string content, string? title = null, IReadOnlyList<FilePickerFileType>? options = null)
    {
        ArgumentNullException.ThrowIfNull(content);
        
        var mainWindow = App.GetMainWindow();
    
        var topLevel = TopLevel.GetTopLevel(mainWindow);
        
        if (topLevel == null)
            return false;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            FileTypeChoices = options
        });

        if (file == null) 
            return false;
        
        await using var stream = await file.OpenWriteAsync();
        await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
        await streamWriter.WriteAsync(content);
        return true;
    }
}