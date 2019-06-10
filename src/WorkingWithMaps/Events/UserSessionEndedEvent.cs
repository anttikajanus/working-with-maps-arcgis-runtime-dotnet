using Prism.Events;

namespace WorkingWithMaps.Example.Events
{
    public class UserSessionEndedEvent : PubSubEvent<UserSessionEndedMessage> {}
}
