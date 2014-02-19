
namespace OpenDDD
{
    public abstract class ValueObject : AssertionConcern
    {
        protected bool PropertiesAreEqual(string propertyName, object objA, object objB)
        {
            if (objA == null || objB == null) return false;

            var propertyInfo = objA.GetType().GetProperty(propertyName);
            var valueA = propertyInfo.GetValue(objA);
            var valueB = propertyInfo.GetValue(objB);

            return AreEqual(valueA, valueB);
        }

        private static bool AreEqual(object objA, object objB)
        {
            if (objA == null || objB == null)
                return false;

            if (objA.GetType() != objB.GetType())
                return false;

            var test = objA == objB;

            return test;
        }
    }
}
