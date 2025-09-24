using System;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using JishoTangoAssistant.Presentation.UI.ViewModels;

namespace JishoTangoAssistant.Presentation.UI;

public class ViewLocator : IDataTemplate
{
    public Control Build(object? data)
    {
        if (data == null)
            return new TextBlock { Text = "data not found" };
        
        var name = data.GetType().FullName?.Replace("ViewModel", "View");

        if (name == null)
            return new TextBlock { Text = "type name of data not found" };
        
        var type = Type.GetType(name);

        if (type != null)
        {
            return (Control)Activator.CreateInstance(type)!;
        }

        return new TextBlock { Text = $"Not Found: {name}" };
    }

    public bool Match(object? data)
    {
        return data is JishoTangoAssistantViewModelBase;
    }
}