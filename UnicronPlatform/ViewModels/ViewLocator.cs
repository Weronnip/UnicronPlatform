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

            var baseViewName = viewModelName
                .Replace("UnicronPlatform.ViewModels.", "")
                .Replace("ViewModel", "");

            string[] viewNamespaces =
            [
                "UnicronPlatform.Views.Student",
                "UnicronPlatform.Views.Instructor",
                "UnicronPlatform.Views.Instructor.Components",
                "UnicronPlatform.Views.Instructor.Page"
            ];

            Type? viewType = null;

            foreach (var ns in viewNamespaces)
            {
                var fullViewName = $"{ns}.{baseViewName}";
                Console.WriteLine($"Trying: {fullViewName}\n");
                Console.WriteLine($"Resolved view model: {viewModelName}");
                Console.WriteLine($"Trying base view name: {baseViewName}");
                Console.WriteLine($"Trying full view name: {fullViewName}\n");
                viewType = Assembly.GetExecutingAssembly().GetTypes()
                    .FirstOrDefault(x => x.FullName == fullViewName);
                if (viewType != null)
                    break;
            }


            if (viewType == null)
            {
                return new TextBlock { Text = $"Could not find view for: {baseViewName}" };
            }

            return Activator.CreateInstance(viewType) as Control
                   ?? new TextBlock { Text = $"Could not create view for {viewType.FullName}" };
        }

        public bool SupportsRecycling => false;
    }
}