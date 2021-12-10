using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Toolkit.Mvvm.ComponentModel;

namespace PscCloud.Shared.Models.ViewModels
{
    public class ViewModelBase :  ObservableObject
    {
        public void Cleanup()
        {
            throw new NotImplementedException();
        }
    }
}
