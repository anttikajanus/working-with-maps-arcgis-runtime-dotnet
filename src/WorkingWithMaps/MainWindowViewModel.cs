using Prism.Events;
using Prism.Regions;
using System.Threading.Tasks;
using WorkingWithMaps.Example.Core;
using WorkingWithMaps.Example.Events;

namespace WorkingWithMaps.Example
{
    public class MainWindowViewModel : BaseViewModel
    {
        public MainWindowViewModel(IApplicationService applicationService) : base(applicationService)
        {
            Title = "Naperville Water Utilities";
            RequiresLogin = true;

            ApplicationServices.EventAggregator.GetEvent<UserSessionCreatedEvent>().Subscribe(args => { RequiresLogin = false; });
            ApplicationServices.EventAggregator.GetEvent<UserSessionEndedEvent>().Subscribe(args => { RequiresLogin = true; });
        }

        private bool _reqiresLogin;
        public bool RequiresLogin
        {
            get { return _reqiresLogin; }
            set { SetProperty(ref _reqiresLogin, value); }
        }

        private string _title = string.Empty;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }
    }
}
