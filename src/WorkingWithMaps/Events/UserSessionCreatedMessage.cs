using Esri.ArcGISRuntime.Portal;
using System;

namespace WorkingWithMaps.Example.Events
{
    public class UserSessionCreatedMessage
    {
        public UserSessionCreatedMessage(ArcGISPortal portal)
        {
            Portal = portal ?? throw new ArgumentNullException(nameof(portal));
        }

        public ArcGISPortal Portal { get; }
    }
}
