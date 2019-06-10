using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps.Example.Core.Prism
{
    public class DialogResult : IDialogResult
    {
        public IDialogParameters Parameters { get; } = new DialogParameters();

        public bool? Result { get; }

        public DialogResult() { }

        public DialogResult(bool? result)
        {
            Result = result;
        }

        public DialogResult(bool? result, IDialogParameters parameters)
        {
            Result = result;
            Parameters = parameters;
        }
    }
}
