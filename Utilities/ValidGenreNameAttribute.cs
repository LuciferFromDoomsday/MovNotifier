using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace MovNotifier.Utilities
{
    public class MakeMeUpperCase : ValidationAttribute
    {
        string FirstCharToUpper(string input)
        {
            if (String.IsNullOrEmpty(input))
                throw new ArgumentException("ARGH!");
            return input.First().ToString().ToUpper() + input.Substring(1);
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            try
            {
                validationContext.ObjectType.GetProperty(validationContext.DisplayName)
                .SetValue(validationContext.ObjectInstance, FirstCharToUpper(value.ToString()), null);
            }
            catch (Exception)
            {
            }
            return null;
        }
    }
}
