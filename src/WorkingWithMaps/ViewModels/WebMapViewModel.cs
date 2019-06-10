using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Events;

namespace WorkingWithMaps.Example.ViewModels
{
    public class WebMapViewModel : NavigationViewModel
    {
        public WebMapViewModel(IApplicationService eventAggregator) : base(eventAggregator)
        {
          
        }

        private Map _map = new Map();
        public Map Map
        {
            get { return _map; }
            set { SetProperty(ref _map, value); }
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
        }

        private async Task LoadItemsAsync(ArcGISPortal portal, string webmapId)
        {
            try
            {
                var webmapItem = await PortalItem.CreateAsync(portal, webmapId);
                Map = new Map(webmapItem);
            }
            catch (Exception)
            {

                throw;
            }

        }
    }
}
