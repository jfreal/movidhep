using Movid.Web.Mvc.ModelExtenders.ComplexTypes;
using System;
using System.ComponentModel.DataAnnotations;

namespace Movid.Web.Infrastructure.ViewModelAttributes.ValidationAttributes
{
    /// <summary>
    /// A custom DataAnnotations validation attribute
    /// which makes sure models with a DateRange property 
    /// don't go backwards in time.
    /// </summary>
    public class CantGoBackwardsInTime: ValidationAttribute
    {
        /// <summary>
        /// Validation message pumped up to Html.Validation errors
        /// when the model binder encounters an error.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public override string FormatErrorMessage(string name)
        {
            return "Your date range can't go backwards in time";
            //return base.FormatErrorMessage(name);
        }

        /// <summary>
        /// Checks to make sure we aren't going backwards in time.
        /// </summary>
        /// <param name="value">The DateRange object to validate.</param>
        /// <returns>Whether or not we have the 4.1 gigawatts of power.</returns>
        public override bool IsValid(object value)
        {
            if( !(value is DateRange) )
                throw new InvalidOperationException("This attribute can only be used on DateRange types!");

            var dateRange = value as DateRange;

            return dateRange.End > dateRange.Start;
        }
    }
}