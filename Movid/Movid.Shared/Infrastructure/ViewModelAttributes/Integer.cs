using System.ComponentModel.DataAnnotations;

namespace Movid.Web.Infrastructure.ViewModelExtenders
{
    /// <summary>
    /// The integer.
    /// </summary>
    public class Integer : ValidationAttribute
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="Integer"/> class.
        /// </summary>
        public Integer()
        {
            this.HasRange = false;
        }



        /// <summary>
        /// Gets or sets a value indicating whether a range is to be used
        /// </summary>
        public bool HasRange { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the validated number is nullable
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the Maximum value in the range
        /// </summary>
        public int Max { get; set; }

        /// <summary>
        /// Gets or sets the minimum value in the range
        /// </summary>
        public int Min { get; set; }



        /// <summary>
        /// The format error message.
        /// </summary>
        /// <param name="name">The name of the property being validated</param>
        /// <returns>The formated error message.</returns>
        public override string FormatErrorMessage(string name)
        {
            if (this.HasRange)
            {
                return string.Format("{0} must be a valid number between {1} and {2}.", name, this.Min, this.Max);
            }
            else
            {
                return string.Format("{0} must be a valid number.");
            }
        }

        /// <summary>
        /// Check to determine if the property value is valid
        /// </summary>
        /// <param name="value">The value to check</param>
        /// <returns>true if value is valid, false otherwise</returns>
        public override bool IsValid(object value)
        {
            if (this.IsNullable && value == null)
            {
                return true;
            }
            else if (value == null)
            {
                return false;
            }

            int number;

            if (!int.TryParse(value.ToString(), out number))
            {
                return false;
            }

            return this.HasRange ? number <= this.Max && number >= this.Min : true;
        }

    }
}