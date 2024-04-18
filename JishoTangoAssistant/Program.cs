using Avalonia;
using System;
using JishoTangoAssistant.Interfaces;
using JishoTangoAssistant.Repositories;
using JishoTangoAssistant.Services;
using JishoTangoAssistant.UI;
using JishoTangoAssistant.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

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
        => BuildAvaloniaAppWithServices(BuildServices());
        
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
            .AddScoped<IVocabularyListRepository, VocabularyListRepository>()
            .AddScoped<ICurrentJapaneseUserInputSelectionService, CurrentJapaneseUserInputSelectionService>()
            .AddScoped<IVocabularyListService, VocabularyListService>()
            .AddScoped<IJishoWebService, JishoWebService>()
            .AddSingleton<ViewLocator>();
            
        var services = builder.BuildServiceProvider();

        return services;
    }
}