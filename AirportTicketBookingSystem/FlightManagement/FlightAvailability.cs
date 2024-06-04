using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.CustomValidation;

namespace AirportTicketBookingSystem.FlightManagement
{
    public class FlightAvailability
    {
        [Required(ErrorMessage = "The Flight class is required")]
        public FlightClass FlightClass { get; set; }

        [Required(ErrorMessage = "The Price is required")]
        [Range(10,double.MaxValue, ErrorMessage = "The Price cannot be less than 10")]
        public double Price { 
            
            get; set; }

        [Required(ErrorMessage = "The Total Places is required")]
        [Range(1, int.MaxValue, ErrorMessage = "The Total Places cannot 0")]
        public int TotalPlaces { get; set; }

        [Required(ErrorMessage = "The Available Places is required")]
        [AvailablePlacesLessThanTotalPlaces("TotalPlaces")]
        public int AvailablePlaces { get; set; }

        public FlightAvailability( FlightClass flightClass, double price, int totalPlaces, int availablePlaces)
        {
            FlightClass = flightClass;
            Price = price;
            TotalPlaces = totalPlaces;
            AvailablePlaces = availablePlaces;
        }

        public FlightAvailability() { }


    }
}
