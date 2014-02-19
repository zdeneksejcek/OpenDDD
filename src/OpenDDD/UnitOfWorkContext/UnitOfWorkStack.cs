
using System;

namespace OpenDDD.UnitOfWorkContext
{
    public class UnitOfWorkStack
    {
        private IStack _stack;

        public UnitOfWorkStack()
        {
            _stack = StackFactory.CreateStack();
        }

        public void Push(UnitOfWork uow)
        {
            _stack.Push(uow);
        }

        public UnitOfWork Pop()
        {
            return _stack.Pop();
        }

        public UnitOfWork Peek()
        {
            return _stack.Peek();
        }
    }
}
