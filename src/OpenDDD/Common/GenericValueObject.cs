
namespace OpenDDD.Common
{
    public abstract class GenericValueObject<T> : ValueObject
    {
        protected T Value { get; set; }

        protected GenericValueObject(T value)
        {
            Value = value;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
