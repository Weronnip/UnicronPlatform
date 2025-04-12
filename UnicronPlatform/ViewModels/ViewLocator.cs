using System;
using System.Linq;
using System.Reflection;
using Avalonia.Controls;
using Avalonia.Controls.Templates;
using ReactiveUI;

namespace UnicronPlatform
{
    public class ViewLocator : IDataTemplate
    {
        public bool Match(object? data)
        {
            return data is IRoutableViewModel;
        }

        public Control Build(object? data)
        {
            if (data == null)
                return new TextBlock { Text = "Data is null" };

            var viewModelName = data.GetType().FullName;
            if (string.IsNullOrWhiteSpace(viewModelName))
                return new TextBlock { Text = "Invalid view model" };

            // Заменяем "ViewModels" на "Views" и "ViewModel" на "View"
            var viewName = viewModelName.Replace("UnicronPlatform.ViewModels", "UnicronPlatform.Views.Student")
                .Replace("ViewModel", "");

            var viewType = Assembly.GetExecutingAssembly().GetTypes().FirstOrDefault(x => x.FullName == viewName);
            if (viewType == null)
            {
                return new TextBlock { Text = $"Could not find view for {viewName}" };
            }

            return Activator.CreateInstance(viewType) as Control 
                   ?? new TextBlock { Text = $"Could not create view for {viewName}" };
        }

        public bool SupportsRecycling => false;
    }
}