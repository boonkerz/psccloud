using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PscCloud.ViewModels;

namespace PscCloud.Views
{
    public class SettingsView : UserControl
    {
        public SettingsView()
        {
            InitializeComponent();

        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

       
    }
}
