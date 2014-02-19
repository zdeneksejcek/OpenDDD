using System;

namespace OpenDDD.Common
{
    public class MonetaryValue : ValueObject
    {
        public long Amount { get; private set; }
        public Currency Currency { get; private set; }

        public MonetaryValue(long amount, Currency currency)
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

        public class MathematicalOperationUsedOnMonetaryValuesWithDifferentCurrencies : Exception { }

    }
}