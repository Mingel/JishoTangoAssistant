using Avalonia.Controls;
using JishoTangoAssistantRewrite.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace JishoTangoAssistantRewrite.Services
{
    public class WindowService : IWindowService
    {
        private readonly Dictionary<string, object> viewMap = new Dictionary<string, object>();

        public void ShowWindow(object viewModel)
        {
            var viewModelTypeName = viewModel.GetType().FullName;
            var name = viewModelTypeName?.Replace("ViewModel", "View");

            if (viewModelTypeName != null && viewMap.ContainsKey(name) && ((Window)viewMap[name])?.IsVisible != true)
            {
                ((Window)viewMap[name]).Activate();
            }

            var type = Type.GetType(name);
            var window = Activator.CreateInstance(type) as Window;

            if (window == null)
                return;

            window.DataContext = viewModel;
            window.Show();

            viewMap[name] = window;
        }
    }
}
