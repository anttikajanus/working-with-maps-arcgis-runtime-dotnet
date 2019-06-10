using System;
using System.ComponentModel;
using System.Windows.Controls;

namespace WorkingWithMaps.Example.Core.Prism
{
    public partial class DialogContainer : UserControl, IDialogContainer
    {
        public DialogContainer()
        {
            InitializeComponent();
        }

       // public IDialogResult Result { get; set; }
    }
}
