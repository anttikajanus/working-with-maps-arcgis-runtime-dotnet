using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Portal;
using Prism.Events;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Events;
using WorkingWithMaps.Example.Models;

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

        public ObservableCollection<BookmarkModel> _bookmarks = new ObservableCollection<BookmarkModel>();
        public ObservableCollection<BookmarkModel> Bookmarks
        {
            get { return _bookmarks; }
            set { SetProperty(ref _bookmarks, value); }
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            try
            {
                var webmapItem = navigationContext.Parameters["webmap"] as PortalItem;
                Map = new Map(webmapItem);
                await Map.LoadAsync();
                Bookmarks.Add(
                    new BookmarkModel("Initial viewpoint", Map.InitialViewpoint, BookmarkModel.BookmarkSource.InitialViewpoint));
                Envelope fullExtent = null;
                foreach (var layer in Map.AllLayers)
                {
                    await layer.LoadAsync();
                    if (fullExtent == null)
                        fullExtent = layer.FullExtent;
                    else
                        fullExtent = GeometryEngine.CombineExtents(fullExtent, layer.FullExtent);
                }
                Bookmarks.Add(new BookmarkModel("Full extent", new Viewpoint(fullExtent), BookmarkModel.BookmarkSource.FullExtent));

                foreach (var bookmark in Map.Bookmarks)
                {
                    Bookmarks.Add(new BookmarkModel(bookmark));
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        private async Task LoadMapAsync()
        {
            try
            {
                await Map.LoadAsync();
                List<Task> loadTasks = new List<Task>();
                foreach (var layer in Map.AllLayers)
                {
                    loadTasks.Add(layer.LoadAsync());
                }
                
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
