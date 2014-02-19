namespace OpenDDD
{
    public abstract class TransactionalEventHandler<TEvent> : NonTransactionalEventHandler<TEvent> where TEvent: Event
    {
    }
}
