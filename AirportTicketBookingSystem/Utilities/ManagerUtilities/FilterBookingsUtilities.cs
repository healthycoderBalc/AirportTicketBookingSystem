using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Utilities.BookingUtilities;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;
using AirportTicketBookingSystem.Utilities.UtilitiesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public class FilterBookingsUtilities
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly FilterByFlightUtilities _filterByFlightUtilities;
        private readonly FilterByPassengerUtilities _filterByPassengerUtilities;
        private readonly FilterByFlightClassUtilities _filterByFlightClassUtilities;
        private readonly IUtilities _utilities;

        public FilterBookingsUtilities(IBookingRepository bookingRepository, FilterByFlightUtilities filterByFlightUtilities, FilterByPassengerUtilities filterByPassengerUtilities, FilterByFlightClassUtilities filterByFlightClassUtilities , IUtilities utilities)
        {
            _bookingRepository = bookingRepository;
            _filterByFlightUtilities = filterByFlightUtilities;
            _filterByPassengerUtilities = filterByPassengerUtilities;
            _filterByFlightClassUtilities = filterByFlightClassUtilities;
            _utilities = utilities;
        }

        private static List<string> FilterBookingsOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Flight");
            menu.Add("Price");
            menu.Add("Departure Country");
            menu.Add("Destination Country");
            menu.Add("Departure Date");
            menu.Add("Departure Airport");
            menu.Add("Arrival Airport");
            menu.Add("Passenger");
            menu.Add("Flight Class");

            return menu;
        }

        private void LaunchFilterBookingsSelection(string selection)
        {
            switch (selection)
            {
                // Filter By Flight
                case "1":
                    int flightSelected = _filterByFlightUtilities.GetSelectedFlightId();
                    List<Booking> bookingsByFlight = _bookingRepository.GetBookingsByFlightId(flightSelected);
                    BookingPresentationUtilities.PrintBookings(bookingsByFlight);
                    Console.WriteLine();
                    break;

                // Filter by Price
                case "2":
                    List<double> priceRange = FilterByPriceUtilities.GetSelectedPriceRange();
                    List<Booking> bookingsByPrice = _bookingRepository.GetBookingsByPrice(priceRange[0], priceRange[1]);
                    BookingPresentationUtilities.PrintBookings(bookingsByPrice);
                    Console.WriteLine();
                    break;


                // Filter By Departure Country
                case "3":
                    string departureCountry = FilterByAirportAndCountryUtilities.GetGivenName(false);
                    List<Booking> bookingsByDepartureCountry = _bookingRepository.GetBookingsByDepartureCountry(departureCountry);
                    BookingPresentationUtilities.PrintBookings(bookingsByDepartureCountry);
                    Console.WriteLine();
                    break;


                // Filter By Destination Country
                case "4":
                    string destinationCountry = FilterByAirportAndCountryUtilities.GetGivenName(false);
                    List<Booking> bookingsByDestinationCountry = _bookingRepository.GetBookingsByDestinationCountry(destinationCountry);
                    BookingPresentationUtilities.PrintBookings(bookingsByDestinationCountry);
                    Console.WriteLine();
                    break;

                // Filter By Departure Date
                case "5":
                    List<int> date = FilterByDateUtilities.GetDates();
                    List<Booking> bookingsByDate = _bookingRepository.GetBookingsByDepartureDate(date);
                    BookingPresentationUtilities.PrintBookings(bookingsByDate);
                    Console.WriteLine();
                    break;


                // Filter By Departure Airport
                case "6":
                    string departureAirport = FilterByAirportAndCountryUtilities.GetGivenName(true);
                    List<Booking> bookingsByDepartureAirport = _bookingRepository.GetBookingsByDepartureAirport(departureAirport);
                    BookingPresentationUtilities.PrintBookings(bookingsByDepartureAirport);
                    Console.WriteLine();
                    break;

                // Filter By Arrival Airport
                case "7":
                    string arrivalAirport = FilterByAirportAndCountryUtilities.GetGivenName(true);
                    List<Booking> bookingsByArrivalAirport = _bookingRepository.GetBookingsByArrivalAirport(arrivalAirport);
                    BookingPresentationUtilities.PrintBookings(bookingsByArrivalAirport);
                    Console.WriteLine();
                    break;

                // Filter By Passenger
                case "8":
                    int passengerId = _filterByPassengerUtilities.GetPassengerSelected();
                    List<Booking> bookingsByPassenger = _bookingRepository.GetBookingsByPassengerId(passengerId);
                    BookingPresentationUtilities.PrintBookings(bookingsByPassenger);
                    Console.WriteLine();
                    break;

                // Filter By Flight Class
                case "9":
                    int selectedFlightClass = _filterByFlightClassUtilities.GetFlightClassSelected();
                    List<Booking> bookingsByFlightClass = _bookingRepository.GetBookingsByClass(selectedFlightClass);
                    BookingPresentationUtilities.PrintBookings(bookingsByFlightClass);
                    Console.WriteLine();
                    break;

                //Going back
                case "0":
                    Console.WriteLine();
                    break;

                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    break;

            }
        }
        public void ShowAndLaunchFilterBookingsMenu()
        {
            List<string> menu = FilterBookingsOptions();
            string title = "Please select a parameter to filter bookings";
            string filterBookingOption;
            List<Booking> searchedListOfFlights = _bookingRepository.Bookings;
            do
            {
                Console.WriteLine();
                filterBookingOption = _utilities.ShowMenu(menu, title);

                LaunchFilterBookingsSelection(filterBookingOption);
            } while (filterBookingOption != "0");
        }

    }
}
