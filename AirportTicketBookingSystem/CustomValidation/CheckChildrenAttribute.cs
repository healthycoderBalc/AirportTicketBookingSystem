using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.CustomValidation
{
    public class CheckChildrenAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            GeniusDMValidationResult result = new GeniusDMValidationResult();
            result.ErrorMessage = string.Format(@"Error occured at {0}", validationContext.DisplayName);

            IEnumerable list = value as IEnumerable;
            if (list == null)
            {
                // Single Object
                List<ValidationResult> results = new List<ValidationResult>();
                Validator.TryValidateObject(value, validationContext, results, true);
                result.NestedResults = results;

                return result;
            }
            else
            {
                List<ValidationResult> recursiveResultList = new List<ValidationResult>();

                // List Object
                foreach (var item in list)
                {
                    List<ValidationResult> nestedItemResult = new List<ValidationResult>();
                    ValidationContext context = new ValidationContext(item, validationContext, null);

                    GeniusDMValidationResult nestedParentResult = new GeniusDMValidationResult();
                    nestedParentResult.ErrorMessage = string.Format(@"Error occured at {0}", validationContext.DisplayName);

                    Validator.TryValidateObject(item, context, nestedItemResult, true);
                    nestedParentResult.NestedResults = nestedItemResult;
                    recursiveResultList.Add(nestedParentResult);
                }

                result.NestedResults = recursiveResultList;
                return result;
            }
        }
    }
}
