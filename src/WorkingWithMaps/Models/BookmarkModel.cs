using Esri.ArcGISRuntime.Mapping;
using System;

namespace WorkingWithMaps.Example.Models
{
    /// <summary>
    /// Model for bookmarks which represents a specific location with name. 
    /// </summary>
    /// <remarks>   
    /// Used to abstract <see cref="Bookmark"/> to allow custom bookmarks, ie. bookmarks that arn't saved to the map, like initial extent.
    /// </remarks>
    public class BookmarkModel
    {
        /// <summary>
        /// Defines the source of the bookmark.
        /// </summary>
        public enum BookmarkSource { 
            Bookmark,
            InitialViewpoint,
            FullExtent,
            Custom
        }

        /// <summary>
        /// Create a new instance of the  <see cref="BookmarkModel"/> from given <paramref name="bookmark"/>
        /// </summary>
        /// <param name="bookmark">Used bookmark</param>
        public BookmarkModel(Bookmark bookmark)
        {
            Viewpoint = bookmark.Viewpoint;
            Name = bookmark.Name;
            Source = BookmarkSource.Bookmark;
        }

        /// <summary>
        /// Create a new instance of the <see cref="BookmarkModel"/> from given values.
        /// </summary>
        /// <param name="name">The name of the bookmark.</param>
        /// <param name="viewpoint">The geographic area of the bookmark.</param>
        /// <param name="source">The source of the bookmark.</param>
        public BookmarkModel(string name, Viewpoint viewpoint, BookmarkSource source = BookmarkSource.Bookmark)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Viewpoint = viewpoint ?? throw new ArgumentNullException(nameof(viewpoint));
            Source = source; 
        }

        /// <summary>
        /// Gets the <see cref="Viewpoint"/> of the bookmark.
        /// </summary>
        public Viewpoint Viewpoint { get; }

        /// <summary>
        /// Gets the name of the bookmark.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the source of the bookmark.
        /// </summary>
        public BookmarkSource Source { get; }
    }
}
