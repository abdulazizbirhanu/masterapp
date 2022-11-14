using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Model
{
    public class IntegrationEvent
    {
        public IntegrationEvent() : this(new Guid(), DateTime.Now) { }
        public IntegrationEvent(Guid id,DateTime creationTime)
        {
            this.Id = id;
            this.CreationTime = creationTime;
        }

        [JsonProperty]
        public Guid Id { get; }
        [JsonProperty]
        public DateTime CreationTime { get; }
    }
}
