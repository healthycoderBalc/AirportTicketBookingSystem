using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.CustomValidation
{
    public class AvailablePlacesLessThanTotalPlaces : ValidationAttribute
    {
        private readonly string _comparisonProperty;
        public AvailablePlacesLessThanTotalPlaces(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = (int)value;

            var comparisonValue = (int)validationContext.ObjectType.GetProperty(_comparisonProperty).GetValue(validationContext.ObjectInstance);

            if (currentValue < comparisonValue)
            {
                return new ValidationResult(ErrorMessage = "End date must be later than start date");
            }

            return ValidationResult.Success;
        }

    }
}
