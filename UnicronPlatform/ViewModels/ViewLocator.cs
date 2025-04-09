using System;
using System.Linq;
using Avalonia.Controls;
using Avalonia.ReactiveUI;
using ReactiveUI;

namespace UnicronPlatform
{
    public class ViewLocator : IViewLocator
    {
        public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
        {
            if (viewModel is null)
                return null;

            var viewModelType = viewModel.GetType();

            // 1. Первый вариант: заменяем "ViewModel" на "View"
            var viewTypeName = viewModelType.FullName?.Replace("ViewModel", "View");
            var view = FindView(viewTypeName);

            // 2. Если первый вариант не дал результата, пробуем альтернативное пространство имён.
            // Здесь, например, заменяем пространство "UnicronPlatform.ViewModels" на "UnicronPlatform.Views.Student.Pages"
            if (view == null && viewTypeName != null)
            {
                var alternativeViewTypeName = viewTypeName.Replace("UnicronPlatform.ViewModels", "UnicronPlatform.Views.Student.Pages");
                view = FindView(alternativeViewTypeName);
            }

            return view;
        }

        private IViewFor? FindView(string? viewTypeName)
        {
            if (string.IsNullOrEmpty(viewTypeName))
                return null;

            var viewType = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => a.GetTypes())
                .FirstOrDefault(t => t.FullName == viewTypeName && typeof(IViewFor).IsAssignableFrom(t));

            if (viewType is null)
                return null;

            try
            {
                return (IViewFor?)Activator.CreateInstance(viewType);
            }
            catch
            {
                return null;
            }
        }
    }
}