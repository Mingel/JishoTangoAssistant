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
                desktop.MainWindow = new JishoTangoAssistantWindow();
            }

            base.OnFrameworkInitializationCompleted();
        }

        public static bool UsesDarkMode()
        {
            return Current!.ActualThemeVariant.Key.ToString()!.Equals("Dark");
        }
    }
}
