using NUnit.Framework;

namespace OpenDDD.Tests
{
    [TestFixture]
    public class ValueObjectTests
    {
        [Test]
        public void Equals_Null_ExpectFalse() {
	        var @double = new ValueObjectTestDouble();

            Assert.IsFalse(@double.Equals(null));
        }

        [Test]
        public void Equals_SameObject_ExpectTrue()
        {
            var @double = new ValueObjectTestDouble();

            Assert.IsTrue(@double.Equals(@double));
        }

        public class ValueObjectTestDouble : ValueObject
        {
            public int Id
            {
                get { return 1; }
            }

            public override bool Equals(object obj)
            {
                return base.PropertiesAreEqual("Id", this, obj);
            }
        }

    }
}
