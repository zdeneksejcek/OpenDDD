
using System.Collections.Generic;
using System.Web;

namespace OpenDDD.UnitOfWorkContext
{
    public class InHttpContextStack : IStack
    {
        private const string UNIT_OF_WORK_STACK = "_unit_of_work_stack";
        private Stack<UnitOfWork> Stack
        {
            get
            {
                if (HttpContext.Current.Items[UNIT_OF_WORK_STACK] == null)
                    HttpContext.Current.Items[UNIT_OF_WORK_STACK] = new Stack<UnitOfWork>();

                return HttpContext.Current.Items[UNIT_OF_WORK_STACK] as Stack<UnitOfWork>;
            }
        }

        public void Push(UnitOfWork uow)
        {
            Stack.Push(uow);
        }

        public UnitOfWork Peek()
        {
            return Stack.Count == 0 ? null : Stack.Peek();
        }

        public UnitOfWork Pop()
        {
            return Stack.Count == 0 ? null : Stack.Pop();
        }
    }
}
