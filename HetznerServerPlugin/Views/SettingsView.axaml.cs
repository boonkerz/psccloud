using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using PscCloud.Plugin.HetznerServerPlugin.ViewModels;

namespace PscCloud.Plugin.HetznerServerPlugin.Views
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
            
            this.FindControl<Button>("OpenFile").Click += delegate
            {
                this.OpenFileDialog();  
            };

        }
        
        async protected void OpenFileDialog()
        {
            var dialog = new OpenFileDialog()
            {
                Title = "Open file"
            };

            string[] result = await dialog.ShowAsync((Window)this.VisualRoot);

            if (result.Length > 0)
            {
                ((SettingsViewModel)this.DataContext).SSHFile = result[0];
            }
        }
    }
}
