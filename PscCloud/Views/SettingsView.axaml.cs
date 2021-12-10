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

            this.FindControl<Button>("OpenFile").Click += delegate
            {
                this.OpenFileDialog();  
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
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
