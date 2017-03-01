namespace Liquidacoes.Modules.EventFeed
{
    using System;

    public struct Event
    {
        public long SequenceNumber { get; }
        public DateTime OccuredAt { get; }
        public string Name { get; }
        public object Content { get; }

        public Event(long sequenceNumber, DateTime occuredAt, string name, object content)
        {
            this.SequenceNumber = sequenceNumber;
            this.OccuredAt = occuredAt;
            this.Name = name;
            this.Content = content;
        }
    }
}
