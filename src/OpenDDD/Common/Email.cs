using System;
using System.Text.RegularExpressions;

namespace OpenDDD.Common
{
    public class Email : GenericValueObject<string>
    {
        public string EmailAddress { get { return base.Value; } }

        public Email(string value) : base(value)
        {
            if (!IsValidEmailAddress(value))
                throw new EmailAddressIsNotValid(value);
        }

        public static implicit operator string(Email obj)
        {
            return obj.EmailAddress;
        }

        private bool IsValidEmailAddress(string emailAddress)
        {
            return Regex.IsMatch(emailAddress, @"\A[a-z0-9]+([-._][a-z0-9]+)*@([a-z0-9]+(-[a-z0-9]+)*\.)+[a-z]{2,4}\z")
                   && Regex.IsMatch(emailAddress, @"^(?=.{1,64}@.{4,64}$)(?=.{6,100}$).*");
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
