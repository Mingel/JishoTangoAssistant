using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Avalonia.Themes.Fluent;
using JishoTangoAssistant.UI.View;

namespace JishoTangoAssistant
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new JishoTangoAssisantWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public static bool UsesFluentDarkMode()
        {
            return ((FluentTheme)Current!.Styles[0]).Mode == FluentThemeMode.Dark;
        }
    }
}
