using System;
using System.Collections.Generic;
using OpenDDD.UnitOfWorkContext;

namespace OpenDDD
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly Queue<Event> _events = new Queue<Event>();
        private readonly static UnitOfWorkStack Stack = new UnitOfWorkStack();
        private UnitOfWork ParentUnitOfWork { get; set; }

        public UnitOfWork()
        {
            ParentUnitOfWork = Stack.Peek();
            Stack.Push(this);
        }

        public void Commit()
        {
            if (ParentUnitOfWork != null)
                RegisterEventsInParentUnitOfWork();
            else
                HandleEvents();
        }

        internal void RegisterEvent(Event @event)
        {
            _events.Enqueue(@event);
        }

        private void HandleEvents()
        {
            foreach (var @event in _events)
                EventRegistry.Handle(@event);
        }

        private void RegisterEventsInParentUnitOfWork()
        {
            foreach (var @event in _events)
                ParentUnitOfWork.RegisterEvent(@event);
        }

        public void Dispose()
        {
            Stack.Pop();
        }
    }
}
