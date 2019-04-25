

namespace Brainnom.Core.ObjectSummary
{
    using System.Collections.Generic;

    public class ObjectSummary
    {
        public ObjectSummary()
        {
            Keys = new ObjectSummaryKeys();
            DisplayFields = new List<string>();
        }

        public string DisplayText { get; set; }
        public IEnumerable<string> DisplayFields { get; set; }
        public string EntityTypeName { get; set; }
        public ObjectSummaryKeys Keys { get; set; }
    }
}