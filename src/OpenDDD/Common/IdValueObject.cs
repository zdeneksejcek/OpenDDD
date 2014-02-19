using System;

namespace OpenDDD.Common
{
    public abstract class IdValueObject : GenericValueObject<Guid>
    {
        public Guid Id { get { return Value; }}

        protected IdValueObject(Guid id) : base(id) { }
    }
}
