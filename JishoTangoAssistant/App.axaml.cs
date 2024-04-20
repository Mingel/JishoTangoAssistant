using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using JishoTangoAssistant.UI.Views;
using JishoTangoAssistant.UI.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace JishoTangoAssistant
{
    public class App : Application
    {
        private readonly IServiceProvider serviceCollection;

        public App(IServiceProvider serviceCollection)
        {
            this.serviceCollection = serviceCollection;
        }

        public override void Initialize()
        {
            Resources[typeof(IServiceProvider)] = serviceCollection;
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new JishoTangoAssistantWindowView
                {
                    DataContext = serviceCollection.GetRequiredService<JishoTangoAssistantWindowViewModel>(),
                };
            }

            base.OnFrameworkInitializationCompleted();
        }

        public static bool UsesDarkMode()
        {
            return Current!.ActualThemeVariant.Key.ToString() == "Dark";
        }
    }
}
