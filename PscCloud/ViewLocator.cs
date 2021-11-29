using Avalonia.Controls;
using Avalonia.Controls.Templates;
using PscCloud.ViewModels;
using System;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using PscCloud.Shared.Models.ViewModels;

namespace PscCloud
{
    public class ViewLocator : IDataTemplate
    {
        public bool SupportsRecycling => false;

        public IControl Build(object data)
        {
            var name = data.GetType().FullName!.Replace("ViewModel", "View");
            var type = Type.GetType(name);
            
            if (type != null)
            {
                return (Control)Activator.CreateInstance(type)!;
            }
            else
            {
                if (data is IPluginViewModelBase)
                {
                    return ((IPluginViewModelBase)data).GetViewControl();
                }
                return new TextBlock { Text = "Not Found: " + name };
            }
        }

        public bool Match(object data)
        {
            return data is ViewModelBase;
        }
    }
}
