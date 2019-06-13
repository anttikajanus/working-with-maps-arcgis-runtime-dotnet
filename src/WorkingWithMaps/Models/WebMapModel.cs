using Esri.ArcGISRuntime.Portal;
using Esri.ArcGISRuntime.Tasks.Offline;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps.Example.Models
{
    public class WebMapModel : BindableBase
    {
        private PortalItem _portalItem = null;
        public PortalItem Item
        {
            get { return _portalItem; }
            set { SetProperty(ref _portalItem, value); }
        }

        private ObservableCollection<PreplannedMapArea> _preplannedMapAreas = new ObservableCollection<PreplannedMapArea>();
        public ObservableCollection<PreplannedMapArea> PreplannedMapAreas
        {
            get { return _preplannedMapAreas; }
            set { SetProperty(ref _preplannedMapAreas, value); }
        }

        public bool HasPreplannedMapAreas => PreplannedMapAreas.Any();
    }
}
