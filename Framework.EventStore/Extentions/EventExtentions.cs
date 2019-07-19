using System.Collections.Generic;
using System.Linq;
using System.Text;
using EventStore.ClientAPI;
using Framework.DomainModel.Events;
using Newtonsoft.Json;

namespace Framework.Persistence.EventStore.Extentions
{
    public static class EventExtentions
    {
        public static IEnumerable<EventData> ToEventData(this IEnumerable<IDomainEvent> events)
        {
            return events.Select(q => q.ToEventData());
        }

        public static EventData ToEventData(this IDomainEvent @event)
        {
            var encodedData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));

            return new EventData(@event.EventId, @event.GetType().Name, true, encodedData, null);
        }

        public static IEnumerable<IDomainEvent> ToDomainEvent(this IEnumerable<ResolvedEvent> resolvedEvents)
        {
            return resolvedEvents.Select(q => q.ToDomainEvent());
        }

        public static IDomainEvent ToDomainEvent(this ResolvedEvent resolvedEvent)
        {
            var @event = JsonConvert.DeserializeObject(
                    Encoding.UTF8.GetString(resolvedEvent.Event.Data),
                    TypeResolver.ResolveDomainEventType(resolvedEvent.Event.EventType));

            return (IDomainEvent)@event;
        }
    }
}