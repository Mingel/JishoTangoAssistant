using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using JishoTangoAssistantRewrite.Services;
using JishoTangoAssistantRewrite.ViewModels;
using JishoTangoAssistantRewrite.Views;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace JishoTangoAssistantRewrite;

public partial class App : Application
{
    public override void Initialize()
    {
        var services = new ServiceCollection()
            .AddSingleton<IWindowService, WindowService>()
            .BuildServiceProvider();

        this.Resources[typeof(IServiceProvider)] = services;

        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var windowService = new WindowService();
        var mainViewModel = new MainViewModel(windowService);

        // Line below is needed to remove Avalonia data validation.
        // Without this line you will get duplicate validations from both Avalonia and CT
        BindingPlugins.DataValidators.RemoveAt(0);

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel
            };
        }
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            singleViewPlatform.MainView = new MainView
            {
                DataContext = mainViewModel
            };
        }

        base.OnFrameworkInitializationCompleted();
    }
}
