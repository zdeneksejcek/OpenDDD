
using System;
using System.Collections.Generic;

namespace OpenDDD.UnitOfWorkContext
{
    public class InThreadStack : IStack
    {
        [ThreadStatic]
        private readonly static Stack<UnitOfWork> Stack = new Stack<UnitOfWork>();

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
