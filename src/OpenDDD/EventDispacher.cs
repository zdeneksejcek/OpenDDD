using OpenDDD.UnitOfWorkContext;

namespace OpenDDD
{
    public static class EventDispacher
    {
        public static void Raise(Event @event)
        {
            if (EventDispacherDisabler.IsActive)
                return;

            var unitOfWork = new UnitOfWorkStack().Peek();

            if (unitOfWork != null)
                unitOfWork.RegisterEvent(@event);
            else
                Core.Current.ExecuteEvent(@event);
        }
    }
}