namespace Movid.Web.Mvc.ModelExtenders.ComplexTypes
{
    using System;

    /// <summary>
    /// Used to represent a DateRange, automatically handles the transition from 
    /// a new instance to empty string values for "not set yet" scenarios
    /// </summary>
    public class DateRange
    {
        public string FromDisplayValue
        {
            get
            {
                return Start == DateTime.MinValue ? "" : Start.ToShortDateString();
            }
        }

        public string ToDisplayValue
        {
            get
            {
                return End == DateTime.MinValue ? "" : End.ToShortDateString();
            }
        }


        public DateTime Start { get; set; }
        public DateTime End { get; set; }
    }
}