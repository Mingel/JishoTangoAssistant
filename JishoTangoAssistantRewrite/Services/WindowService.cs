using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using System;
using System.Collections.Generic;

namespace JishoTangoAssistantRewrite.Services
{
    public class WindowService : IWindowService
    {
        private readonly Dictionary<string, object> viewNameWindowMap = new Dictionary<string, object>();

        public void ShowWindow(object viewModel)
        {
            var viewModelTypeName = viewModel.GetType().FullName;
            var viewName = viewModelTypeName?.Replace("ViewModel", "View");


            var desktop = Application.Current?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime;

            if (desktop == null)
                return;

            var mainWindow = desktop.MainWindow;

            if (viewModelTypeName != null && viewNameWindowMap.ContainsKey(viewName))
            {
                ((Window)viewNameWindowMap[viewName]).Activate();
                return;
            }

            var type = Type.GetType(viewName);
            var window = Activator.CreateInstance(type) as Window;

            if (window == null)
                return;

            window.DataContext = viewModel;
            window.Closed += (s, e) => {
                viewNameWindowMap.Remove(viewName);
            };
            window.Show();

            viewNameWindowMap[viewName] = window;
        }
    }
}
