using Avalonia.Collections;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;

namespace PscCloud.Plugin.HetznerServerPlugin.Models
{
    public class TabViewControl : ReactiveObject
    {

        private TabItem selectedTabServer;

        private AvaloniaList<TabItem> tabItems;

        public TabViewControl()
        {
            this.TabItems = new AvaloniaList<TabItem>();
        }

        public TabItem SelectedTabServer
        {
            get => selectedTabServer;
            set => this.RaiseAndSetIfChanged(ref selectedTabServer, value);
        }

        public AvaloniaList<TabItem> TabItems
        {
            get => tabItems;
            set => this.RaiseAndSetIfChanged(ref tabItems, value);
        }

        public void OnCloseTab()
        {
            this.TabItems.Remove(this.SelectedTabServer);
            if (this.TabItems.Count != 0)
            {
                this.SelectedTabServer = this.TabItems[this.TabItems.Count - 1];
            }
        }
    }
}
