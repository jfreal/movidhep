

namespace Brainnom.Core.ObjectSummary
{
    public class RegisteredDescriptionProvider
    {
        public IDescriptionProvider Provider { get; set; }
        public bool Breaking { get; set; }
        public int Order { get; set; }
    }
}