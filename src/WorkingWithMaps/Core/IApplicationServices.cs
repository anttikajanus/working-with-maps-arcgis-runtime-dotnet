using MaterialDesignThemes.Wpf;
using Prism.Commands;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps.Example.Core
{
    public interface IApplicationService
    {
        IEventAggregator EventAggregator { get; }
        INavigationService NavigationService { get; }
        IDialogService DialogService { get; }
        IConfigurationService ConfigurationService { get; }
    }

    public class ApplicationService : IApplicationService
    {
        public ApplicationService(IEventAggregator eventAggregator, INavigationService navigationService, IConfigurationService configurationService, IDialogService dialogService)
        {
            EventAggregator = eventAggregator ?? throw new ArgumentNullException(nameof(eventAggregator));
            NavigationService = navigationService ?? throw new ArgumentNullException(nameof(navigationService));
            ConfigurationService= configurationService ?? throw new ArgumentNullException(nameof(configurationService));
            DialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        public IEventAggregator EventAggregator { get; }

        public INavigationService NavigationService { get; }

        public IDialogService DialogService { get; }

        public IConfigurationService ConfigurationService { get; }
    }

    public interface INavigationService
    {
        DelegateCommand GoForwardCommand { get; }
        DelegateCommand GoBackCommand { get; }
        void RequestNavigation(string targetView, NavigationParameters parameters);
    }

    public class NavigationService : INavigationService
    {
        private IRegionManager _regionManager;
 
        public NavigationService(IRegionManager regionManager)
        {
            _regionManager = regionManager ?? throw new ArgumentNullException(nameof(regionManager));

            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
            GoBackCommand = new DelegateCommand(GoBack, CanGoBack);

            MainRegionName = "MainContentRegion";
            OverlayRegionName = "OverlayContentRegion";
        }

        public DelegateCommand GoForwardCommand { get; }
        public DelegateCommand GoBackCommand { get; }

        public string MainRegionName { get; set; }
        public string OverlayRegionName { get; set; }

        public DelegateCommand<string> NavigateToCommand { get; }

        private void GoForward()
        {
            if (!CanGoBack())
            {
                return;
            }
            (var region, var journal) = GetMainRegion();
            journal.GoForward();
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        private bool CanGoForward()
        {
            (var region, var journal) = GetMainRegion();
            if (region == null || journal == null)
                return false;
            return journal != null && journal.CanGoForward;
        }

        private void GoBack()
        {
            if (!CanGoBack())
            {
                return;
            }
            (var region, var journal) = GetMainRegion();
            journal.GoBack();
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }

        private bool CanGoBack()
        {
            (var region, var journal) = GetMainRegion();
            if (region == null || journal == null)
                return false;
            return journal != null && journal.CanGoBack;
        }

        public void RequestNavigation(string targetView, NavigationParameters parameters)
        {
            _regionManager.RequestNavigate(MainRegionName, targetView, parameters);
            GoBackCommand.RaiseCanExecuteChanged();
            GoForwardCommand.RaiseCanExecuteChanged();
        }


        private (IRegion, IRegionNavigationJournal) GetMainRegion()
        {
            if (!_regionManager.Regions.Any())
                return (null, null);

            var region = _regionManager.Regions[MainRegionName];
            return (region, region.NavigationService.Journal);
        }
    }
}
