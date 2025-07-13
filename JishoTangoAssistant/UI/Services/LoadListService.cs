using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using JishoTangoAssistant.Common.Data;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Utils;
using Newtonsoft.Json;

namespace JishoTangoAssistant.UI.Services;

public class LoadListService(IVocabularyListService vocabularyListService, ICurrentSessionService currentSessionService, WindowManipulatorService windowManipulatorService)
{
    public async Task PerformLoad()
    {
        bool? performOverwriting = null;
        if (vocabularyListService.Count() > 0)
        {
            var msgBoxResult = await MessageBoxUtil.CreateAndShowAsync("Warning", "Your vocabulary list is not empty." + Environment.NewLine + "Do you want to overwrite or merge into your current vocabulary list?",
                MessageBoxButtons.MergeOverwriteCancel);

            if (msgBoxResult == MessageBoxResult.Cancel)
                return;
            performOverwriting = msgBoxResult == MessageBoxResult.Overwrite;
        }

        var filePickerTitle = performOverwriting switch
        {
            true => "Open file to load vocabulary list (Overwrite)",
            false => "Open file to load vocabulary list (Merge)",
            _ => "Open file to load vocabulary list"
        };

        var filePickerFilter = new[] {
            new FilePickerFileType("JTA Files") { Patterns = ["*.jta"] }
        };

        var startLocationPath = Path.GetDirectoryName(currentSessionService.GetLoadedFilePath());
        var loadedFileInfo = await FilePicker.LoadAsync<VocabularyItem>(filePickerTitle, filePickerFilter, startLocationPath);
        var loadedVocabularyItems = loadedFileInfo?.Content;
        
        // this case can occur if user cancels file dialog
        if (loadedVocabularyItems == null)
            return;

        if (performOverwriting == true)
            await vocabularyListService.ClearAsync();
        await vocabularyListService.AddRangeAsync(loadedVocabularyItems, true);
        currentSessionService.SetLoadedFilePath(loadedFileInfo?.FilePath);
        currentSessionService.SetUserMadeUnsavedChanges(false);
        windowManipulatorService.UpdateTitle();
    }

    public async Task<IEnumerable<VocabularyItem>?> LoadFromFile(string absoluteFilePath)
    {
        var filePath = Uri.UnescapeDataString(absoluteFilePath);

        if (!File.Exists(filePath))
            return null;

        await using var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        using var streamReader = new StreamReader(stream, Encoding.UTF8);
        var fileTextContent = await streamReader.ReadToEndAsync();
        
        var loadedFileTextInfo = new FileInfo<string>(fileTextContent, filePath);
        
        var fileContent = loadedFileTextInfo.Content;
        var content = JsonConvert.DeserializeObject<VocabularyItem[]>(fileContent);

        return content;
    }
}