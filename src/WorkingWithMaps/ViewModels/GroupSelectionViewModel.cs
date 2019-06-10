using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Esri.ArcGISRuntime.Portal;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Core.Prism;
using WorkingWithMaps.Example.Events;

namespace WorkingWithMaps.Example.ViewModels
{
    public class GroupSelectionViewModel : NavigationViewModel
    {
        public GroupSelectionViewModel(IApplicationService eventAggregator) : base(eventAggregator)
        {
            NavigateCommand = new DelegateCommand<PortalGroup>(Navigate);
        }

        public DelegateCommand<PortalGroup> NavigateCommand { get; }

        private ObservableCollection<PortalGroup> _groups = default;
        public ObservableCollection<PortalGroup> Groups
        {
            get { return _groups; }
            set { SetProperty(ref _groups, value); }
        }

        private void Navigate(PortalGroup group)
        {
            var parameters = new NavigationParameters
                {
                    { "group", group}
                };
            ApplicationServices.NavigationService.RequestNavigation("WebMapsView", parameters);
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            var portal = navigationContext.Parameters["portal"] as ArcGISPortal;
            var groupIds= ApplicationServices.ConfigurationService.GetSetting("GroupIds").Split(',');
            var groups = portal.User.Groups.Where(x => { return groupIds.Contains(x.GroupId); });
            Groups = new ObservableCollection<PortalGroup>(groups);

            base.OnNavigatedTo(navigationContext);
        }
    }
}
