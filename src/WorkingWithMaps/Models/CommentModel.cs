using Esri.ArcGISRuntime.Portal;
using Prism.Mvvm;
using System;

namespace WorkingWithMaps.Example.Models
{
    public class CommentModel : BindableBase
    {
        private readonly PortalItemComment _comment;

        public CommentModel(PortalItemComment comment)
        {
            _comment = comment ?? throw new ArgumentNullException(nameof(comment));
            CommentText = Uri.UnescapeDataString(_comment.Comment);
        }

        public string CommentText { get; }

        public DateTimeOffset Created => _comment.Created;

        public string Owner => _comment.Owner;
    }
}
