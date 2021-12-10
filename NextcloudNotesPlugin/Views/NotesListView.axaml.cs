using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.ReactiveUI;
using PscCloud.Plugin.Nextcloud.Notes.ViewModels;
using ReactiveUI;

namespace PscCloud.Plugin.Nextcloud.Notes.Views
{
    public class NotesListView : ReactiveUserControl<NotesListViewModel>
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
