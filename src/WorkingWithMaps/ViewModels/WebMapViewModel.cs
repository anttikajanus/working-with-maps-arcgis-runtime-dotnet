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
        public WebMapViewModel(IApplicationService applicationServices) : base(applicationServices)
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
            try
            {
                var webmapItem = navigationContext.Parameters["webmap"] as PortalItem;
                Map = new Map(webmapItem);
                base.OnNavigatedTo(navigationContext);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
