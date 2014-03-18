using System;
using System.Runtime.InteropServices;

namespace OpenDDD.Common
{
    public abstract class IdValueObject : GenericValueObject<Guid>
    {
        public Guid Id { get { return Value; }}

        protected IdValueObject(Guid id) : base(id) { }

        public static implicit operator Guid(IdValueObject obj)
        {
            return obj.Id;
        }
    }
}
