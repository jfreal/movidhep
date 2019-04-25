
namespace Movid.Web.Conventions.Validation
{
    using System.ComponentModel.DataAnnotations;
    using System.Text.RegularExpressions;

    public class ValidatePhoneNumber : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return base.IsValid(value, validationContext);
        }

        public override bool IsValid(object value)
        {
            //required or not is not this attributes responsiblity
            if (value == null)
                return true;
            
            if( string.IsNullOrWhiteSpace(value.ToString() ) )
                return true;

            var isValid = Regex.Match(value.ToString(), @"^([0-9]( |-)?)?(\(?[0-9]{3}\)?|[0-9]{3})( |-)?([0-9]{3}( |-)?[0-9]{4}|[a-zA-Z0-9]{7})$").Success;

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Please enter a valid " + name;
        }
    }
}