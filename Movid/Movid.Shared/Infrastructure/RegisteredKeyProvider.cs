

using Brainnom.Core.ObjectSummary;

namespace Movid.Web.Infrastructure
{
    public class RegisteredKeyProvider
    {
        public IKeyProvider Provider { get; set; }
        public bool Breaking { get; set; }
        public int Order { get; set; }
    }
}