namespace OpenDDD
{
    public class DomainExceptionHasBeenThrown : Event
    {
        public string Description { get; private set; }

        public DomainExceptionHasBeenThrown(string description)
        {
            Description = description;
        }
    }
}
