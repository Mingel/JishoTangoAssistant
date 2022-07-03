using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace JishoTangoAssistant
{
    public partial class MainWindow : Window
    {
        public static MainWindow Instance;
        
        public MainWindow()
        {
            Instance = this;
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
