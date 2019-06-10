using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorkingWithMaps.Example.Core.Prism
{
    internal static class IDialogWindowExtensions
    {
        internal static IDialogAware GetDialogViewModel(this IDialogContainer dialogHost)
        {
            return (IDialogAware)dialogHost.DataContext;
        }
    }
}
