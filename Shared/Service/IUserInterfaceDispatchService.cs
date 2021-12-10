using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PscCloud.Shared.Service
{
    public interface IUserInterfaceDispatchService
    {
        /// <summary>
        /// Invokes an action sychronously on the UI dispatcher.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        void Invoke(Action action);

        /// <summary>
        /// Invokes an action asychronously on the UI dispatcher.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        Task InvokeAsync(Action action);
    }
}
