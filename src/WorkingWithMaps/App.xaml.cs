using Prism.Ioc;
using System.Windows;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Core.Prism;
using WorkingWithMaps.Example.Views;
using WorkingWithMaps.Example.Views.Dialogs;

namespace WorkingWithMaps.Example
{
    public partial class App 
    {
        public App()
        {
        }

        protected override Window CreateShell()
        {
            var shell = Container.Resolve<MainWindow>();
            return shell;
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // Services
            containerRegistry.RegisterSingleton<IDialogService, DialogService>();
            containerRegistry.RegisterSingleton<IConfigurationService, ConfigurationService>();
            containerRegistry.RegisterSingleton<INavigationService, NavigationService>();
            containerRegistry.RegisterSingleton<IApplicationService, ApplicationService>();

            // Views
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<WebMapsView>();
            containerRegistry.RegisterForNavigation<WebMapView>();
            containerRegistry.RegisterForNavigation<GroupSelectionView>();


            // Dialogs
            containerRegistry.RegisterDialogContainer<DialogContainer>();
            containerRegistry.RegisterDialog<WebMapDetailsDialogView>("WebMapDetailsDialog");
        }

        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
        }
    }
}
