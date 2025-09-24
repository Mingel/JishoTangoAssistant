using System;
using Avalonia;
using Avalonia.Svg.Skia;
using JishoTangoAssistant.Application.Core.Interfaces;
using JishoTangoAssistant.Application.Core.Services;
using JishoTangoAssistant.Infrastructure.ApiClients;
using JishoTangoAssistant.Infrastructure.Persistence.Repositories;
using JishoTangoAssistant.Presentation.UI;
using JishoTangoAssistant.Presentation.UI.Services;
using JishoTangoAssistant.Presentation.UI.ViewModels.VocabularyListViewModels;
using JishoTangoAssistant.Repositories;
using Microsoft.Extensions.DependencyInjection;
using JapaneseUserInputViewModel = JishoTangoAssistant.Presentation.UI.ViewModels.JapaneseUserInputViewModels.JapaneseUserInputViewModel;
using JishoTangoAssistantWindowViewModel = JishoTangoAssistant.Presentation.UI.ViewModels.JishoTangoAssistantWindowViewModel;

namespace JishoTangoAssistant.Presentation;

internal class Program
{
    // Initialization code. Don't use any Avalonia, third-party APIs or any
    // SynchronizationContext-reliant code before AppMain is called: things aren't initialized
    // yet and stuff might break.
    [STAThread]
    public static void Main(string[] args) => BuildAvaloniaApp()
        .StartWithClassicDesktopLifetime(args);

    // Avalonia configuration, don't remove; also used by visual designer.
    public static AppBuilder BuildAvaloniaApp()
    {
        GC.KeepAlive(typeof(SvgImageExtension).Assembly);
        GC.KeepAlive(typeof(Avalonia.Svg.Skia.Svg).Assembly);
        return BuildAvaloniaAppWithServices(BuildServices());
    }

    public static AppBuilder BuildAvaloniaAppWithServices(IServiceProvider serviceProvider)
        => AppBuilder.Configure(() => new App(serviceProvider))
            .UsePlatformDetect()
            .LogToTrace();

    private static ServiceProvider BuildServices()
    {
        var builder = new ServiceCollection()
            .AddSingleton<JishoTangoAssistantWindowViewModel>()
            .AddSingleton<JapaneseUserInputViewModel>()
            .AddSingleton<VocabularyListViewModel>()
            .AddSingleton<SaveListUiService>()
            .AddScoped<IFileService, FileService>()
            .AddScoped<ICurrentSessionRepository, CurrentSessionRepository>()
            .AddScoped<IVocabularyListRepository, VocabularyListRepository>()
            .AddScoped<ICurrentJapaneseUserInputSelectionService, CurrentJapaneseUserInputSelectionService>()
            .AddScoped<ICurrentSessionService, CurrentSessionService>()
            .AddScoped<IVocabularyListService, VocabularyListService>()
            .AddScoped<IJishoRepository, JishoRepository>()
            .AddSingleton<ViewLocator>()
            .AddHttpClient();
            
        var services = builder.BuildServiceProvider();

        return services;
    }
}