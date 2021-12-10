using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PscCloud.Plugin.Nextcloud.Notes.ViewModels;

namespace PscCloud.Plugin.Nextcloud.Notes.Views
{
    public class SettingsView : ReactiveWindow<SettingsViewModel>
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
