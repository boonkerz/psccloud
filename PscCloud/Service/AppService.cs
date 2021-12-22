using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Avalonia;
using Avalonia.Collections;
using DynamicData;
using Microsoft.Toolkit.Mvvm.DependencyInjection;
using Prise;
using PscCloud.Plugin.Contract;
using PscCloud.Shared.Service;
using PscCloud.Shared.Settings;

namespace PscCloud.Service
{
    public class AppService
    {
        public event EventHandler AppStarted;
        public event EventHandler AppStartSyncing;
        public event EventHandler AppEndSyncing;
        
        protected virtual void OnAppStarted(EventArgs e)
        {
            AppStarted?.Invoke(this, e);
        }
        protected virtual void OnAppEndSyncing(EventArgs e)
        {
            AppEndSyncing?.Invoke(this, e);
        }
        protected virtual void OnAppStartSyncing(EventArgs e)
        {
            AppStartSyncing?.Invoke(this, e);
        }
        public void AppIsStartSyncing()
        {
            OnAppStartSyncing(EventArgs.Empty);
        }
        public void AppIsEndSyncing()
        {
            OnAppEndSyncing(EventArgs.Empty);
        }
        public void AppIsStarted()
        {
            OnAppStarted(EventArgs.Empty);
        }
        
    }
}