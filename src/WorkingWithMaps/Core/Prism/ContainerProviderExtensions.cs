using Prism.Ioc;

namespace WorkingWithMaps.Example.Core.Prism
{
    public static class IContainerRegistryExtensions
    {
        /// <summary>
        /// Registers an object to be used as a dialog in the IDialogService.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the dialog</typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="name">The unique name to register with the dialog.</param>
        public static void RegisterDialog<TView>(this IContainerRegistry containerRegistry, string name = null)
        {
            containerRegistry.RegisterForNavigation<TView>(name);
        }

        /// <summary>
        /// Registers an object to be used as a dialog in the IDialogService.
        /// </summary>
        /// <typeparam name="TView">The Type of object to register as the dialog</typeparam>
        /// <typeparam name="TViewModel">The ViewModel to use as the DataContext for the dialog</typeparam>
        /// <param name="containerRegistry"></param>
        /// <param name="name">The unique name to register with the dialog.</param>
        public static void RegisterDialog<TView, TViewModel>(this IContainerRegistry containerRegistry, string name = null)
            where TViewModel : IDialogAware
        {
            containerRegistry.RegisterForNavigation<TView, TViewModel>(name);
        }

        /// <summary>
        /// Registers an object that implements IDialogContainer to be used to host all dialogs in the IDialogService.
        /// </summary>
        /// <typeparam name="TWindow">The Type of the container class that will be used to host dialogs in the IDialogService</typeparam>
        /// <param name="containerRegistry"></param>
        public static void RegisterDialogContainer<TWindow>(this IContainerRegistry containerRegistry) where TWindow : IDialogContainer
        {
            containerRegistry.Register(typeof(IDialogContainer), typeof(TWindow));
        }
    }
}
