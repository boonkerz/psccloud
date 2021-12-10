using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using PscCloud.Plugin.HetznerServerPlugin.Models;
using PscCloud.Plugin.HetznerServerPlugin.ViewModels;
using PscCloud.ViewModels;

namespace PscCloud.Plugin.HetznerServerPlugin.Views
{
    public class ServerListView : UserControl
    {
        public ServerListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void InputElement_OnDoubleTapped(object? sender, RoutedEventArgs e)
        {
            var vm = (ServerListViewModel)this.DataContext;
            if (typeof(DataGrid).IsInstanceOfType(sender))
            {
                DataGrid grid = (DataGrid)sender;
                if (typeof(Server).IsInstanceOfType(grid.SelectedItem))
                {
                    vm.addTab((Server)grid.SelectedItem);
                }
            }
        }
    }
}
