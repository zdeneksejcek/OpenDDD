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
            if (Core.Current == null)
                throw new Exception("OpenDDD UnitOfWork cannot be used without initialized Core");

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
                Core.Current.ExecuteEvent(@event);
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