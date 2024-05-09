using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.CustomValidation
{
    public class GeniusDMValidationResult : ValidationResult
    {
        public GeniusDMValidationResult() : base("")
        {

        }
        public IList<ValidationResult> NestedResults { get; set; }
    }
}
