using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Platform.Storage;
using JishoTangoAssistant.Models;
using Newtonsoft.Json;

namespace JishoTangoAssistant.Utils;

public class VocabularyListFilePicker {
    public static async Task<IEnumerable<VocabularyItem>?> LoadAsync(string? title = null, IReadOnlyList<FilePickerFileType>? options = null)
    {
        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;
    
        var topLevel = TopLevel.GetTopLevel(mainWindow);
        
        if (topLevel == null)
            return null;
            
        var files = await topLevel.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = title,
            AllowMultiple = false,
            FileTypeFilter = options
        });

        if (files.Count >= 1)
        {
            await using var stream = await files[0].OpenReadAsync();
            using var streamReader = new StreamReader(stream);
            var fileContent = await streamReader.ReadToEndAsync();
            return JsonConvert.DeserializeObject<VocabularyItem[]>(fileContent);
        }
        return null;
    }
    
    // return true, if saving was successful
    public static async Task<bool> SaveAsync(IEnumerable<VocabularyItem> items, string? title = null, IReadOnlyList<FilePickerFileType>? options = null)
    {
        var json = JsonConvert.SerializeObject(items, Formatting.Indented);
        return await SaveAsync(json, title, options); 
    }
    
    // return true, if saving was successful
    public static async Task<bool> SaveAsync(string content, string? title = null, IReadOnlyList<FilePickerFileType>? options = null)
    {
        ArgumentNullException.ThrowIfNull(content);
        
        var mainWindow = ((IClassicDesktopStyleApplicationLifetime)Avalonia.Application.Current?.ApplicationLifetime!).MainWindow;
    
        var topLevel = TopLevel.GetTopLevel(mainWindow);
        
        if (topLevel == null)
            return false;

        var file = await topLevel.StorageProvider.SaveFilePickerAsync(new FilePickerSaveOptions
        {
            Title = title,
            FileTypeChoices = options
        });

        if (file != null)
        {
            await using var stream = await file.OpenWriteAsync();
            await using var streamWriter = new StreamWriter(stream, Encoding.UTF8);
            await streamWriter.WriteAsync(content);
            return true;
        }
        
        return false;
    }
}