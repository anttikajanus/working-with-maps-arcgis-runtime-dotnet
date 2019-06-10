using Esri.ArcGISRuntime.Portal;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Core.Prism;
using WorkingWithMaps.Example.Events;

namespace WorkingWithMaps.Example.ViewModels
{
    public class WebMapsViewModel : NavigationViewModel
    {
        public WebMapsViewModel(IApplicationService applicationService) : base(applicationService)
        {
        }

        private ObservableCollection<PortalItem> _webMaps = new ObservableCollection<PortalItem>();
        public ObservableCollection<PortalItem> WebMaps
        {
            get { return _webMaps; }
            set { SetProperty(ref _webMaps, value); }
        }

        public async override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var group = navigationContext.Parameters["group"] as PortalGroup;
            await LoadItemsAsync(group);

            var parameters = new DialogParameters
                {
                    { "group", group}
                };

            var dialogResult = await ApplicationServices.DialogService.ShowAsync("PortalGroupDetailsDialog", parameters, (result) => {
                Debug.WriteLine($"result {result.Result} with parameters: {result.Parameters.ToString()}");
            });

            base.OnNavigatedTo(navigationContext);
        }

        private async Task LoadItemsAsync(PortalGroup group)
        {
            //group:01b41abf1d574a3190439e7fdd967762
            var results = await group.Portal.FindItemsAsync(new PortalQueryParameters($@"type:'web map' AND  group: {group.GroupId}")
            {
                CanSearchPublic = false,  // Find only items from used portal
                Limit = 20,
                SortField = "relevance"
            }); 
            foreach (var item in results.Results)
            {
                WebMaps.Add(item);
            }
        }
    }
}
