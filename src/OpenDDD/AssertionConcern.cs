using System;

namespace OpenDDD
{
    public class AssertionConcern
    {
        public static void AssertArgumentEquals(object obj1, object obj2, string message)
        {
            if (!obj1.Equals(obj2))
                throw new ArgumentException(message);
        }

        public static void AssertArgumentEquals<T>(object obj1, object obj2, T exception) where T : Exception, new()
        {
            if (!obj1.Equals(obj2))
                throw new T();
        }

        public static void AssertArgumentFalse(bool value, string message)
        {
            if (value)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentLength(string value, int maximum, string message)
        {
            int length = value.Trim().Length;
            if (length > maximum)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentLength(string value, int minimum, int maximum, string message)
        {
            var length = value.Trim().Length;
            if (length < minimum || length > maximum)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentNotEmpty(string value, string message)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException(message);
        }

        public static void AssertArgumentNotEquals(object obj1, object obj2, string message)
        {
            if (obj1.Equals(obj2))
                throw new ArgumentException(message);
        }

        public static void AssertArgumentNotNull(object value, string message)
        {
            if (value == null)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentRange(double value, double minimum, double maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentRange(float value, float minimum, float maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentRange(int value, int minimum, int maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentRange(long value, long minimum, long maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentRange(decimal value, decimal minimum, decimal maximum, string message)
        {
            if (value < minimum || value > maximum)
                throw new ArgumentException(message);
        }

        public static void AssertArgumentTrue(bool value, string message)
        {
            if (!value)
                throw new ArgumentException(message);
        }

        public static void AssertStateFalse(bool aBoolean, string message)
        {
            if (aBoolean)
                throw new InvalidOperationException(message);
        }

        public static void AssertStateTrue(bool aBoolean, string message)
        {
            if (!aBoolean)
                throw new InvalidOperationException(message);
        }
    }
}
