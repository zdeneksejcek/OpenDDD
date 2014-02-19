using System.Web;

namespace OpenDDD.EventQueue
{
    public class EventQueueFactory
    {
        public static IEventQueue CreatEventQueue()
        {
            if (HttpContext.Current != null)
                return new InHttpRequestEventQueue();

            return new InThreadEventQueue();
        }
    }
}
