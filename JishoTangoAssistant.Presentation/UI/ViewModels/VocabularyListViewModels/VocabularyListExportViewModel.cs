using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Presentation.UI.Enums;
using JishoTangoAssistant.Presentation.UI.Services;
using JishoTangoAssistant.Presentation.UI.Utils;
using JishoTangoAssistant.Shared.Constants;

namespace JishoTangoAssistant.Presentation.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListExportViewModel : JishoTangoAssistantViewModelBase
{
    private readonly ICurrentSessionService currentSessionService;
    private readonly SaveListUiService saveListUiService;

    public VocabularyListExportViewModel(ICurrentSessionService currentSessionService, SaveListUiService saveListUiService)
    {
        this.currentSessionService = currentSessionService;
        this.saveListUiService = saveListUiService;
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
        var isSuccessful = await saveListUiService.PerformExportAsCsv(toJapanese);
        if (isSuccessful)
            await ShowNotetypeMessageBox();
    }

    private static async Task ShowNotetypeMessageBox()
    {
        await MessageBoxUtil.CreateAndShowAsync("Information", "Export complete!",
            MessageBoxButtons.Ok);
    }
}