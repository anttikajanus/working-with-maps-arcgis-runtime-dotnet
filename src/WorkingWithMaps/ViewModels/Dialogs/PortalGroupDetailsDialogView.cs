using Esri.ArcGISRuntime.Portal;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Core.Prism;
using WorkingWithMaps.Example.Events;

namespace WorkingWithMaps.Example.ViewModels.Dialogs
{
    public class PortalGroupDetailsDialogViewModel : DialogViewModel
    {
        public PortalGroupDetailsDialogViewModel(IApplicationService applicationService) : base(applicationService)
        {
            Title = "Details";
        }

        private PortalGroup _group = default;
        public PortalGroup Group
        {
            get { return _group; }
            set { SetProperty(ref _group, value); }
        }

        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var group = parameters.GetValue<PortalGroup>("group");
            Group = group;
        }

        protected override void CloseDialog(string parameter)
        {
            DialogResult result = default;
            switch (parameter)
            {
                case "true":
                    var parameters = new DialogParameters
                    {
                        { "hello", "world" }
                    };
                    result = new DialogResult(true, parameters);
                    break;
                case "false":
                    result = new DialogResult(false);
                    break;
            }
            RaiseRequestClose(result);
        }

        public override bool CanCloseDialog()
        {
            return true;
        }

        public override void OnDialogClosed()
        {
            Debug.WriteLine("Dialog closed.");
        }
    }
}
