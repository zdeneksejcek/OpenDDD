using OpenDDD.UnitOfWorkContext;

namespace OpenDDD
{
    public class CommandDispacher
    {
        public static void Send(Command command)
        {
            if (EventDispacherDisabler.IsActive)
                return;

            var unitOfWork = new UnitOfWorkStack().Peek();

            if (unitOfWork != null)
                unitOfWork.RegisterCommand(command);
            else
                Core.Current.Execute(command);
        }
    }
}
