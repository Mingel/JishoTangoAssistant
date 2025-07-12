using Avalonia;
using System;
using JishoTangoAssistant.UI;
using JishoTangoAssistant.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Avalonia.Svg.Skia;
using JishoTangoAssistant.Core.Interfaces;
using JishoTangoAssistant.Core.Services;
using JishoTangoAssistant.Infrastructure.ApiClients;
using JishoTangoAssistant.Infrastructure.Persistence.Repositories;
using JishoTangoAssistant.UI.Services;
using JishoTangoAssistant.UI.ViewModels.VocabularyListViewModels;
using JapaneseUserInputViewModel = JishoTangoAssistant.UI.ViewModels.JapaneseUserInputViewModels.JapaneseUserInputViewModel;

namespace JishoTangoAssistant;

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
            .AddSingleton<WindowManipulatorService>()
            .AddSingleton<SaveListService>()
            .AddSingleton<LoadListService>()
            .AddScoped<ICurrentSessionRepository, CurrentSessionRepository>()
            .AddScoped<IVocabularyListRepository, VocabularyListRepository>()
            .AddScoped<ICurrentJapaneseUserInputSelectionService, CurrentJapaneseUserInputSelectionService>()
            .AddScoped<ICurrentSessionService, CurrentSessionService>()
            .AddScoped<IVocabularyListService, VocabularyListService>()
            .AddScoped<IJishoWebService, JishoWebService>()
            .AddSingleton<ViewLocator>()
            .AddHttpClient();
            
        var services = builder.BuildServiceProvider();

        return services;
    }
}