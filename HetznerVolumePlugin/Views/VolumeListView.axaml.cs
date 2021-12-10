using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PscCloud.Plugin.HetznerVolumePlugin.Views
{
    public class VolumeListView : UserControl
    {
        public VolumeListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
