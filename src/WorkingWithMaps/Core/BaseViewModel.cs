using Prism.Events;
using Prism.Mvvm;
using Prism.Regions;
using System;

namespace WorkingWithMaps.Example.Core
{
    public class BaseViewModel : BindableBase
    {
        public BaseViewModel(IApplicationService applicationService)
        {
            ApplicationServices = applicationService ?? throw new ArgumentNullException(nameof(applicationService));
        }

        public IApplicationService ApplicationServices { get; }
    }
}
