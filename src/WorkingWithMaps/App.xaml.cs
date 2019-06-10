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
            containerRegistry.RegisterSingleton<IDialogService, DialogService>();
            containerRegistry.RegisterSingleton<IConfigurationService, ConfigurationService>();
            containerRegistry.RegisterSingleton<INavigationService, NavigationService>();
            containerRegistry.RegisterSingleton<IApplicationService, ApplicationService>();
            containerRegistry.RegisterForNavigation<LoginView>();
            containerRegistry.RegisterForNavigation<WebMapsView>();
            containerRegistry.RegisterForNavigation<GroupSelectionView>();

            containerRegistry.RegisterDialogWindow<DialogContainer>();
            containerRegistry.RegisterDialog<PortalGroupDetailsDialogView>("PortalGroupDetailsDialog");
        }

        protected override void ConfigureServiceLocator()
        {
            base.ConfigureServiceLocator();
        }
    }
}
