using Avalonia.Collections;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using DynamicData;

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

        public void OnCloseTab(string id)
        {

            if (SelectedTabServer.ServerItem.Id.ToString() == id)
            {
                this.TabItems.Remove(this.SelectedTabServer);
                if (this.TabItems.Count != 0)
                {
                    this.SelectedTabServer = this.TabItems[this.TabItems.Count - 1];
                } 
            }
            else
            {
                var items = this.tabItems.Where(x => x.ServerItem.Id.ToString() == id).ToList();
                this.TabItems.RemoveAll(items);
            }
        }

        public void AddTab(TabItem tabItem)
        {
            if (this.TabItems.Where(x => x.ServerItem.Id == tabItem.ServerItem.Id).Count() > 0)
            {

                var items = this.TabItems.Where(x => x.ServerItem.Id == tabItem.ServerItem.Id);
                this.SelectedTabServer = items.First();
            }
            else
            {
                this.TabItems.Add(tabItem);
                this.SelectedTabServer = tabItem;
            }
        }
    }
}
