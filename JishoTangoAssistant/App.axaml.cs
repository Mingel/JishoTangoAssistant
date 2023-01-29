using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
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

        public static bool UsesDarkMode()
        {
            return ((Avalonia.Themes.Simple.SimpleTheme)(Current!.Styles[0])).Mode == Avalonia.Themes.Simple.SimpleThemeMode.Dark;
        }
    }
}
