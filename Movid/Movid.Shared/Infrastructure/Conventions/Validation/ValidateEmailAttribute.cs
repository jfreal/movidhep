using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Movid.Web.Conventions
{
    public class ValidateEmail : RequiredAttribute
    {
        public ValidateEmail() : base() { }

        public override bool IsValid(object value)
        {
            if (value == null)
                return false;
            
            if( string.IsNullOrWhiteSpace(value.ToString() ) )
                return false;

            var isValid =  Regex.Match(value.ToString(), @"(\w+)@(\w+)\.(\w+)").Success;

            return isValid;
        }

        public override string FormatErrorMessage(string name)
        {
            return "Please enter a valid email address";
        }
    }
}