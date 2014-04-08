using System;
using System.Collections.Generic;
using OpenDDD.UnitOfWorkContext;

namespace OpenDDD
{
    public sealed class UnitOfWork : IDisposable
    {
        private readonly Queue<Event> _events = new Queue<Event>();
        private readonly Queue<Command> _commands = new Queue<Command>();
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
            {
                RegisterEventsInParentUnitOfWork();
                RegisterCommandsInParentUnitOfWork();
            }
            else
            {
                HandleEvents();
                HandleCommands();
            }
        }

        internal void RegisterEvent(Event @event)
        {
            _events.Enqueue(@event);
        }

        internal void RegisterCommand(Command command)
        {
            _commands.Enqueue(command);
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

        private void HandleCommands()
        {
            foreach (var command in _commands)
                Core.Current.Execute(command);
        }

        private void RegisterCommandsInParentUnitOfWork()
        {
            foreach (var command in _commands)
                ParentUnitOfWork.RegisterCommand(command);
        }

        public void Dispose()
        {
            Stack.Pop();
        }
    }
}