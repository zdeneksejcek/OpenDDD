using System;

namespace OpenDDD
{
    public abstract class DomainException : Exception
    {
        protected DomainException()
        {
            //EventDispacher.Raise(new DomainExceptionHasBeenThrown(GetDescription()));
        }
    }
}