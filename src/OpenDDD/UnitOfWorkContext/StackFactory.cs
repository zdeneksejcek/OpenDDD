
using System.Web;

namespace OpenDDD.UnitOfWorkContext
{
    public class StackFactory
    {
        public static IStack CreateStack()
        {
            if (HttpContext.Current != null)
                return new InHttpContextStack();

            return new InThreadStack();
        }
    }
}
