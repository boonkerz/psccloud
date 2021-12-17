using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PscCloud.Plugin.Nextcloud.Notes.Views
{
    public class SettingsView : Window
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
