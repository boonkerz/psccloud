using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace PscCloud.Plugin.Nextcloud.Notes.Views
{
    public class NotesListView : UserControl
    {
        public NotesListView()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
