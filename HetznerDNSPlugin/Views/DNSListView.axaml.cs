using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PscCloud.Plugin.HetznerDNSPlugin.Views
{
    public class DNSListView : UserControl
    {
        public DNSListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
