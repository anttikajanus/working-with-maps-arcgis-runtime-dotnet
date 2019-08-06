using Esri.ArcGISRuntime.Portal;
using Prism.Commands;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Models;

namespace WorkingWithMaps.Example.ViewModels
{
    public class WebMapsViewModel : NavigationViewModel
    {
        public WebMapsViewModel(IApplicationService applicationService) : base(applicationService)
        {
            NavigateToDetailsCommand = new DelegateCommand<WebMapModel>(NavigateToDetails);
            NavigateToOnlineMapCommand = new DelegateCommand<WebMapModel>(NavigateToOnlineMap);
            NavigateToOfflineSelectionCommand = new DelegateCommand<WebMapModel>(NavigateToOfflineSelection);
        }

        public DelegateCommand<WebMapModel> NavigateToDetailsCommand { get; }
        public DelegateCommand<WebMapModel> NavigateToOnlineMapCommand { get; }
        public DelegateCommand<WebMapModel> NavigateToOfflineSelectionCommand { get; }

        private ObservableCollection<PortalItem> _webMaps = new ObservableCollection<PortalItem>();
        public ObservableCollection<PortalItem> WebMaps
        {
            get { return _webMaps; }
            set { SetProperty(ref _webMaps, value); }
        }

        private ObservableCollection<WebMapModel> _webMapModels = new ObservableCollection<WebMapModel>();
        public ObservableCollection<WebMapModel> WebMapModels
        {
            get { return _webMapModels; }
            set { SetProperty(ref _webMapModels, value); }
        }

        private void NavigateToDetails(WebMapModel model)
        {
            var parameters = new DialogParameters
                {
                    { "model", model}
                };

            ApplicationServices.DialogService.ShowDialog("WebMapDetailsDialog", parameters, null);
        }

        private void NavigateToOnlineMap(WebMapModel model)
        {
            var parameters = new DialogParameters
                {
                    { "webmap", model.Item}
                };

            ApplicationServices.NavigationService.RequestNavigation("WebMapView", parameters);
        }

        private void NavigateToOfflineSelection(WebMapModel model)
        {
            var parameters = new DialogParameters
                {
                    { "model", model}
                };

           ApplicationServices.DialogService.ShowDialog("WebMapDetailsDialog", parameters, null);
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var group = navigationContext.Parameters["group"] as PortalGroup;
            WebMaps.Clear();
            WebMapModels.Clear();
            await LoadItemsAsync(group);
            base.OnNavigatedTo(navigationContext);
        }

        private async Task LoadItemsAsync(PortalGroup group)
        {
            var results = await group.Portal.FindItemsAsync(new PortalQueryParameters($@"type:'web map' AND  group: {group.GroupId}")
            {
                CanSearchPublic = false,  // Find only items from used portal
                Limit = 20,
                SortField = "relevance"
            }); 
            foreach (var item in results.Results)
            {
                WebMaps.Add(item);

                // Test
                var model = new WebMapModel
                {
                    Item = item
                };
                WebMapModels.Add(model);
            }
        }
    }
}
