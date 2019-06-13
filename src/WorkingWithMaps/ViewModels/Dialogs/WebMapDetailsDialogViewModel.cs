using Esri.ArcGISRuntime.Portal;
using System.Collections.ObjectModel;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Core.Prism;
using WorkingWithMaps.Example.Models;

namespace WorkingWithMaps.Example.ViewModels.Dialogs
{
    public class WebMapDetailsDialogViewModel : DialogViewModel
    {
        public WebMapDetailsDialogViewModel(IApplicationService applicationService) : base(applicationService)
        {
            Title = "Details";
        }

        private PortalItem _item = null;
        public PortalItem Item
        {
            get { return _item; }
            set { SetProperty(ref _item, value); }
        }

        private ObservableCollection<CommentModel> _comments = new ObservableCollection<CommentModel>();
        public ObservableCollection<CommentModel> Comments
        {
            get { return _comments; }
            set { SetProperty(ref _comments, value); }
        }

        public async override void OnDialogOpened(IDialogParameters parameters)
        {
            var model = parameters.GetValue<WebMapModel>("model");
            Item = model.Item;

            var comments = await Item.GetCommentsAsync();
            foreach (var comment in comments)
            {
                Comments.Add(new CommentModel(comment));
            }
        }
    }
}
