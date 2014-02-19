using System;

namespace OpenDDD
{
    public abstract class AssertionConcern
    {
        protected void AssertArgumentEquals(object obj1, object obj2, string message)
        {
            if (!obj1.Equals(obj2))
                throw new ArgumentException(message);
        }

        protected static void AssertArgumentEquals<T>(object obj1, object obj2, T exception) where T : Exception, new()
        {
            if (!obj1.Equals(obj2))
                throw new T();
        }

        protected void AssertArgumentFalse(bool value, string message)
        {
            if (value)
                throw new ArgumentException(message);
        }

        protected void AssertArgumentLength(string value, int maximum, string message)
        {
            int length = value.Trim().Length;
            if (length > maximum)
                throw new ArgumentException(message);
        }

        protected void assertArgumentLength(string value, int minimum, int maximum, string message)
        {
            var length = value.Trim().Length;
            if (length < minimum || length > maximum)
                throw new ArgumentException(message);
        }

        protected void assertArgumentNotEmpty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(message);
        }

        protected void assertArgumentNotEquals(object obj1, object obj2, string message)
        {
            if (obj1.Equals(obj2))
                throw new ArgumentException(message);
        }

        protected void assertArgumentNotNull(object value, string message)
        {
            if (value == null)
                throw new ArgumentException(message);
        }

        protected void assertArgumentRange(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        protected void assertArgumentRange(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        protected void assertArgumentRange(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        protected void assertArgumentRange(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        protected void assertArgumentTrue(bool value, string message)
        {
            if (!value)
                throw new ArgumentException(message);
        }

        protected void assertStateFalse(bool aBoolean, string message)
        {
            if (aBoolean)
                throw new InvalidOperationException(message);
        }

        protected void assertStateTrue(bool aBoolean, string message)
        {
            if (!aBoolean)
                throw new InvalidOperationException(message);
        }
    }
}
