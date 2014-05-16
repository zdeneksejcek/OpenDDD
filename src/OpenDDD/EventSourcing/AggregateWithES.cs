using System.Collections.Generic;

namespace OpenDDD.EventSourcing
{
    public abstract class AggregateWithES : Aggregate
    {
        private List<Event> _commitedEvents = new List<Event>();
        private List<Event> _uncommitedEvents = new List<Event>();

        protected AggregateWithES() {}

        protected AggregateWithES(IEnumerable<Event> commitedEvents)
        {
            _commitedEvents.AddRange(commitedEvents);
        }

        protected void ApplyCommited(IEnumerable<Event> events)
        {
            foreach (var @event in events)
            {
                
            }
        }
    }

    public class SalesOrder : AggregateWithES
    {
        public int TaxId { get; private set; }

        public void ChangeTax(int taxId)
        {
            Apply(new TaxChanged(taxId));
        }

        private void Apply(TaxChanged @event)
        {
            
        }
    }

    public class TaxChanged
    {
        public int TaxId { get; private set; }

        public TaxChanged(int taxId)
        {
            TaxId = taxId;
        }
    }

}