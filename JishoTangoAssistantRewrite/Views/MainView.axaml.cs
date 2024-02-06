using Avalonia.Controls;
using JishoTangoAssistantRewrite.ViewModels;

namespace JishoTangoAssistantRewrite.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        JishoWordSearch = new JishoWordSearchViewModel();
        JishoQueryInfo = new JishoQueryInfoViewModel();
    }

    public JishoWordSearchViewModel JishoWordSearch { get; }
    public JishoQueryInfoViewModel JishoQueryInfo { get; }
}
