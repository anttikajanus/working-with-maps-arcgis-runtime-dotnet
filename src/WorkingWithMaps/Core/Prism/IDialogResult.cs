using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps.Example.Core.Prism
{
    public interface IDialogResult
    {
        /// <summary>
        /// The parameters from the dialog
        /// </summary>
        IDialogParameters Parameters { get; }

        /// <summary>
        /// The result of the dialog.
        /// </summary>
        bool? Result { get; }
    }
}
