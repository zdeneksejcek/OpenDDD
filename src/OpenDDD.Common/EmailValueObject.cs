using System;

namespace OpenDDD.Common
{
    public class EmailValueObject : GenericValueObject<string>
    {
        public string EmailAddress { get { return base.Value; } }

        public EmailValueObject(string value) : base(value)
        {
            if (!IsValidEmailAddress(value))
                throw new EmailAddressIsNotValid(value);
        }

        private bool IsValidEmailAddress(string emailAddress)
        {
            return true;
        }

        protected override int GetHashCodeModifier()
        {
            return 57*15;
        }

        public class EmailAddressIsNotValid : Exception
        {
            public string InvalidEmailAddress { get; private set; }
            public EmailAddressIsNotValid(string invalidEmailAddress)
            {
                InvalidEmailAddress = invalidEmailAddress;
            }
        }
    }
}
