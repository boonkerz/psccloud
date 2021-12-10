using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PscCloud.Views
{
    public class PluginListView : UserControl
    {
        public PluginListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
