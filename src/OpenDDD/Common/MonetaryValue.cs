using System;
using System.Diagnostics;

namespace OpenDDD.Common
{
    [DebuggerDisplay("{Currency} {Amount}")]
    public class MonetaryValue : ValueObject
    {
        public decimal Amount { get; private set; }
        public Currency Currency { get; private set; }

        public MonetaryValue(decimal amount, Currency currency)
        {
            this.Amount = amount;
            this.Currency = currency;
        }

        public static MonetaryValue operator +(MonetaryValue a, MonetaryValue b)
        {
            if (a.Currency != b.Currency)
                throw new MathematicalOperationUsedOnMonetaryValuesWithDifferentCurrencies();

            return new MonetaryValue(a.Amount + b.Amount, a.Currency);
        }

        public override bool Equals(object obj)
        {
            if (!(obj != null && obj.GetType() == typeof (MonetaryValue)))
                return false;

            var value = (MonetaryValue) obj;

            return (value.Amount == Amount && value.Currency == Currency);
        }

        public override int GetHashCode()
        {
            return Amount.GetHashCode() + Currency.GetHashCode();
        }

        public class MathematicalOperationUsedOnMonetaryValuesWithDifferentCurrencies : Exception { }

    }
}