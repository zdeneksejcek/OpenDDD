namespace OpenDDD.UnitOfWorkContext
{
    public interface IStack
    {
        void Push(UnitOfWork uof);
        UnitOfWork Peek();
        UnitOfWork Pop();
    }
}
