using System;
using System.Web;

namespace OpenDDD
{
    public class EventDispacherDisabler : IDisposable
    {
        public EventDispacherDisabler()
        {
            EventDisablerStatusFactory.GetStatus().IsActive = true;
        }

        public static bool IsActive
        {
            get { return EventDisablerStatusFactory.GetStatus().IsActive; }
        }

        public void Dispose()
        {
            EventDisablerStatusFactory.GetStatus().IsActive = false;
        }

        internal class EventDisablerStatusFactory
        {
            public static IStatus GetStatus()
            {
                if (HttpContext.Current == null)
                    return new InThreadStatus();

                return new InHttpContextStatus();
            }
        }

        internal interface IStatus
        {
            bool IsActive { get; set; }
        }

        internal class InThreadStatus : IStatus
        {
            [ThreadStatic]
            private static bool Active;

            public bool IsActive
            {
                get { return Active; }
                set { Active = value; }
            }
        }

        internal class InHttpContextStatus : IStatus
        {
            public bool IsActive
            {
                get { return HttpContext.Current.Items.Contains("_eventDisablerStatus"); }
                set
                {
                    if (value)
                        HttpContext.Current.Items["_eventDisablerStatus"] = true;
                    else
                        HttpContext.Current.Items.Remove("_eventDisablerStatus");
                }

            }
        }

    }
}