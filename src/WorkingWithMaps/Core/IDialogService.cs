using MaterialDesignThemes.Wpf;
using Prism.Common;
using Prism.Ioc;
using System;
using System.Threading.Tasks;
using System.Windows;
using WorkingWithMaps.Example.Core.Prism;

namespace WorkingWithMaps.Example.Core
{
    public interface IDialogService
    {
        Task<IDialogResult> ShowAsync(string name, IDialogParameters parameters, Action<IDialogResult> callback);
    }

    public class DialogService : IDialogService
    {
        private readonly IContainerExtension _containerExtension;
     
        public DialogService(IContainerExtension containerExtension)
        {
            _containerExtension = containerExtension;
        }
        public Task<IDialogResult> ShowAsync(string name, IDialogParameters parameters, Action<IDialogResult> callback = null)
        {
            return ShowDialogInternal(name, parameters, callback);
        }

        private async Task<IDialogResult> ShowDialogInternal(string name, IDialogParameters parameters, Action<IDialogResult> callback)
        {
            IDialogContainer dialogWindow = CreateDialogWindow();
            ConfigureDialogContent(name, dialogWindow, parameters);

            void requestCloseHandler(IDialogResult result)
            {
                if (!dialogWindow.GetDialogViewModel().CanCloseDialog())
                    return;

                DialogHost.CloseDialogCommand.Execute(result, null);
                dialogWindow.GetDialogViewModel().OnDialogClosed();
                callback?.Invoke(result);
                dialogWindow.GetDialogViewModel().RequestClose -= requestCloseHandler;
            }
            dialogWindow.GetDialogViewModel().RequestClose += requestCloseHandler;

            var dialogResult = await DialogHost.Show(dialogWindow) as IDialogResult;
            return dialogResult;
        }

        private IDialogContainer CreateDialogWindow()
        {
            return _containerExtension.Resolve<IDialogContainer>();
        }

        private void ConfigureDialogContent(string dialogName, IDialogContainer windowContainer, IDialogParameters parameters)
        {
            var content = _containerExtension.Resolve<object>(dialogName);
            var dialogContent = content as FrameworkElement;
            if (dialogContent == null)
                throw new NullReferenceException("A dialog's content must be a FrameworkElement");

            var viewModel = dialogContent.DataContext as IDialogAware;
            if (viewModel == null)
                throw new NullReferenceException("A dialog's ViewModel must implement the IDialogAware interface");

            windowContainer.Content = dialogContent;
            windowContainer.DataContext = viewModel;

            MvvmHelpers.ViewAndViewModelAction<IDialogAware>(viewModel, d => d.OnDialogOpened(parameters));
        }
    }
}