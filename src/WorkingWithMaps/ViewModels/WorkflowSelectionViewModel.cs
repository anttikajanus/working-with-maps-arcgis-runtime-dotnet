using System;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Portal;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Events;

namespace WorkingWithMaps.Example.ViewModels
{
    public class WorkflowSelectionViewModel : NavigationViewModel
    {
        public WorkflowSelectionViewModel(IApplicationService eventAggregator) : base(eventAggregator)
        {
            ApplicationServices.EventAggregator.GetEvent<UserSessionCreatedEvent>().Subscribe(async args =>
            {
                await LoadItemsAsync(args.Portal);
            });
        }

        private PortalItem _waterHydrantInspectionsItem;
        public PortalItem WaterHydrantInspectionsItem
        {
            get { return _waterHydrantInspectionsItem; }
            set { SetProperty(ref _waterHydrantInspectionsItem, value); }
        }

        private async Task LoadItemsAsync(ArcGISPortal portal)
        {
            try
            {
                var waterHydrantInspectionsItemId = "c9fbe40a90a7474988bbba12fe7de04d";
                WaterHydrantInspectionsItem = await PortalItem.CreateAsync(portal, waterHydrantInspectionsItemId);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
