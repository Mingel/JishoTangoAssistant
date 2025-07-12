using System;
using System.IO;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;
using CommunityToolkit.Mvvm.Input;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Models;
using JishoTangoAssistant.UI.Elements;
using JishoTangoAssistant.UI.Services;
using JishoTangoAssistant.UI.Utils;

namespace JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;

public partial class VocabularyListLoadViewModel(LoadListService loadListService) : JishoTangoAssistantViewModelBase
{
    [RelayCommand]
    private async Task LoadList() => await loadListService.PerformLoad();
}