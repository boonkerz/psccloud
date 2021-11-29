using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PscCloud.Shared.Service;

namespace PscCloud.Service
{
    public class AvaloniaDispatcherService : IUserInterfaceDispatchService
    {
        /// <inheritdoc/>
        public void Invoke(Action action)
        {
            Avalonia.Threading.Dispatcher.UIThread.Post(action);
        }

        /// <inheritdoc/>
        public Task InvokeAsync(Action action)
        {
            return Avalonia.Threading.Dispatcher.UIThread.InvokeAsync(action);
        }
    }
}
