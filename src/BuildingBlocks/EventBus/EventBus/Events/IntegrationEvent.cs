using System.Text.Json.Serialization;

namespace EventBus.Events
{
    public class IntegrationEvent
    {
        [JsonInclude]
        public Guid Id { get; private set; }

        [JsonInclude]
        public DateTime CreationDate { get; private set; }

        public IntegrationEvent()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationEvent(Guid id, DateTime creationDate)
        {
            Id = id;
            CreationDate = creationDate;
        }
    }
}