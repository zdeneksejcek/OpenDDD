namespace OpenDDD.Messaging
{
    public interface IMessageQueue
    {
        void Enqueue(IMessage message);
    }
}