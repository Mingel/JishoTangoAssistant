using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Common.Utils;
using JishoTangoAssistant.Core.Constants;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Utils;

namespace JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListExportViewModel : JishoTangoAssistantViewModelBase
{
    private readonly IVocabularyListService vocabularyListService;
    private readonly ICurrentSessionService currentSessionService;

    public VocabularyListExportViewModel(IVocabularyListService vocabularyListService, ICurrentSessionService currentSessionService)
    {
        this.vocabularyListService = vocabularyListService;
        this.currentSessionService = currentSessionService;
    }
    
    [Range(6, 96, ErrorMessage = "Value must be between 6 and 96, currently set to default value")]
    public int FontSize
    {
        get => currentSessionService.GetExportSettings().FontSize;
        set
        {
            var exportSettings = currentSessionService.GetExportSettings();
            var fontSize = Math.Clamp(value, Constants.MinFontSize, Constants.MaxFontSize);
            if (fontSize != exportSettings.FontSize)
                currentSessionService.SetExportSettings(exportSettings with { FontSize = fontSize });
        }
    }
    
    [RelayCommand]
    private async Task ExportCsvJapaneseToEnglish()
    {
        await ExportCsv(false);
    }

    [RelayCommand]
    private async Task ExportCsvEnglishToJapanese()
    {
        await ExportCsv(true);
    }

    private async Task ExportCsv(bool toJapanese)
    {
        var filePickerFilter = new[] {
            new FilePickerFileType("CSV Files") { Patterns = ["*.csv"] }
        };

        var list = vocabularyListService.GetList();

        var contentToExport = toJapanese ? VocabularyListExporter.EnglishToJapanese(list, currentSessionService.GetExportSettings()) : VocabularyListExporter.JapaneseToEnglish(list, currentSessionService.GetExportSettings());

        var result = await FilePicker.SaveAsync(contentToExport, "Export vocabulary list as", filePickerFilter);

        if (result != null)
            await ShowNotetypeMessageBox();
    }

    private static async Task ShowNotetypeMessageBox()
    {
        await MessageBoxUtil.CreateAndShowAsync("Information", "Export complete!",
            MessageBoxButtons.Ok);
    }
}