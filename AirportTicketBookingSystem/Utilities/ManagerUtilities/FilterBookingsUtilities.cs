using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class FilterBookingsUtilities
    {
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

        private static void LaunchFilterBookingsSelection(string selection)
        {
            switch (selection)
            {
                // Filter By Flight
                case "1":
                    int flightSelected = FilterByFlightUtilities.GetSelectedFlightId();
                    List<Booking> bookingsByFlight = BookingRepository.GetBookingsByFlightId(flightSelected);
                    BookingRepository.PrintBookings(bookingsByFlight);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;

                // Filter by Price
                case "2":
                    List<double> priceRange = FilterByPriceUtilities.GetSelectedPriceRange();
                    List<Booking> bookingsByPrice = BookingRepository.GetBookingsByPrice(priceRange[0], priceRange[1]);
                    BookingRepository.PrintBookings(bookingsByPrice);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;


                // Filter By Departure Country
                case "3":
                    string departureCountry = FilterByAirportAndCountryUtilities.GetGivenName(false);
                    List<Booking> bookingsByDepartureCountry = BookingRepository.GetBookingsByDepartureCountry(departureCountry);
                    BookingRepository.PrintBookings(bookingsByDepartureCountry);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;


                // Filter By Destination Country
                case "4":
                    string destinationCountry = FilterByAirportAndCountryUtilities.GetGivenName(false);
                    List<Booking> bookingsByDestinationCountry = BookingRepository.GetBookingsByDestinationCountry(destinationCountry);
                    BookingRepository.PrintBookings(bookingsByDestinationCountry);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;

                // Filter By Departure Date
                case "5":
                    List<int> date = FilterByDateUtilities.GetDates();
                    List<Booking> bookingsByDate = BookingRepository.GetBookingsByDepartureDate(date);
                    BookingRepository.PrintBookings(bookingsByDate);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;


                // Filter By Departure Airport
                case "6":
                    string departureAirport = FilterByAirportAndCountryUtilities.GetGivenName(true);
                    List<Booking> bookingsByDepartureAirport = BookingRepository.GetBookingsByDepartureAirport(departureAirport);
                    BookingRepository.PrintBookings(bookingsByDepartureAirport);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;

                // Filter By Arrival Airport
                case "7":
                    string arrivalAirport = FilterByAirportAndCountryUtilities.GetGivenName(true);
                    List<Booking> bookingsByArrivalAirport = BookingRepository.GetBookingsByArrivalAirport(arrivalAirport);
                    BookingRepository.PrintBookings(bookingsByArrivalAirport);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;

                // Filter By Passenger
                case "8":
                    int passengerId = FilterByPassengerUtilities.GetPassengerSelected();
                    List<Booking> bookingsByPassenger = BookingRepository.GetBookingsByPassengerId(passengerId);
                    BookingRepository.PrintBookings(bookingsByPassenger);
                    Console.WriteLine();
                    Console.ReadLine();
                    break;

                // Filter By Flight Class
                case "9":
                    int selectedFlightClass = FilterByFlightClassUtilities.GetFlightClassSelected();
                    List<Booking> bookingsByFlightClass = BookingRepository.GetBookingsByClass(selectedFlightClass);
                    BookingRepository.PrintBookings(bookingsByFlightClass);
                    Console.WriteLine();
                    Console.ReadLine();
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
        public static void ShowAndLaunchFilterBookingsMenu()
        {
            List<string> menu = FilterBookingsOptions();
            string title = "Please select a parameter to filter bookings";
            string filterBookingOption;
            List<Booking> searchedListOfFlights = BookingRepository.Bookings;
            do
            {
                Console.WriteLine();
                filterBookingOption = Utilities.ShowMenu(menu, title);

                LaunchFilterBookingsSelection(filterBookingOption);
            } while (filterBookingOption != "0");
        }

    }
}
