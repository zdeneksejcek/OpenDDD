
using System;

namespace OpenDDD.Common
{
    public class DateTimeUtc : ValueObject
    {
        public DateTime Value { get; private set; }

        public DateTimeUtc(DateTime value)
        {
            if (value.Kind != DateTimeKind.Utc)
                throw new DoesNotSupportNonUtcValue();

            this.Value = value;
        }

        public static DateTimeUtc Convert(DateTime? value)
        {
            if (value == null)
                return null;

            return new DateTimeUtc(value.Value);
        }

        public class DoesNotSupportNonUtcValue : Exception { }

    }
}
