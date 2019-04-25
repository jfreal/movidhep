namespace Movid.App.Models
{
    public class MarketingPlan
    {
        private string[] _filterRule;
        public bool Enabled { get; set; }
        public string Greeting { get; set; }
        public PatientStatus PatientStatus { get; set; }
        public int DaysAfter { get; set; }
        
        public TriggerOption TriggerOptions { get; set; }
        public string[] FilterRule
        {
            get { return _filterRule ?? (_filterRule = new string[0]); }
            set { _filterRule = value; }
        }
    }
}