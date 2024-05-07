using AirportTicketBookingSystem.FlightManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities
{
    public static class SearchFlightUtilities
    {
        // ******************************************
        // Search Flights
        // ******************************************

        private static List<string> SearchFlightsOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("Price");
            menu.Add("Departure Country");
            menu.Add("Destination Country");
            menu.Add("Departure Date");
            menu.Add("Departure Airport");
            menu.Add("Arrival Airport");
            menu.Add("Flight Class");
            menu.Add("Clear Search");
            menu.Add("Search Ready, Select a flight");

            return menu;
        }

        private static object[] LaunchSearchFlightsSelection(string selection, List<string> filters, List<Flight> searchedListOfFlights)
        {
            object[] result = [filters, searchedListOfFlights];
            switch (selection)
            {
                // Search By Price
                case "1":
                    List<double> priceRange = SearchByPrice("Price");
                    searchedListOfFlights = FlightsInventory.SearchFlightsByPrice(searchedListOfFlights, priceRange);
                    FlightsInventory.ShowFlights(searchedListOfFlights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    ((List<string>)result[0]).Add("Price");
                    result[1] = searchedListOfFlights;
                    return result;

                // Search By Departure Country
                case "2":
                    string departureCountry = SearchBy("Departure Country");
                    searchedListOfFlights = FlightsInventory.SearchFlightsByAirportOrCountryName(searchedListOfFlights, "DepartureAirport", departureCountry, true);
                    FlightsInventory.ShowFlights(searchedListOfFlights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    ((List<string>)result[0]).Add("Departure Country");
                    result[1] = searchedListOfFlights;
                    return result;


                // Search By Destination Country
                case "3":
                    string destinationCountry = SearchBy("Destination Country");
                    searchedListOfFlights = FlightsInventory.SearchFlightsByAirportOrCountryName(searchedListOfFlights, "ArrivalAirport", destinationCountry, true);
                    FlightsInventory.ShowFlights(searchedListOfFlights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    ((List<string>)result[0]).Add("Departure Country");
                    result[1] = searchedListOfFlights;
                    return result;


                // Search By Departure Date
                case "4":
                    List<int> date = SearchByDate("Date");
                    searchedListOfFlights = FlightsInventory.SearchFlightsByDate(searchedListOfFlights, date);
                    FlightsInventory.ShowFlights(searchedListOfFlights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    ((List<string>)result[0]).Add("Date");
                    result[1] = searchedListOfFlights;
                    return result;


                // Search By Departure Airport
                case "5":
                    string departureAirport = SearchBy("Departure Airport");
                    searchedListOfFlights = FlightsInventory.SearchFlightsByAirportOrCountryName(searchedListOfFlights, "DepartureAirport", departureAirport, false);
                    FlightsInventory.ShowFlights(searchedListOfFlights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    ((List<string>)result[0]).Add("Departure Airport");
                    result[1] = searchedListOfFlights;
                    return result;

                // Search By Arrival Airport
                case "6":
                    string arrivalAirport = SearchBy("Arrival Airport");
                    searchedListOfFlights = FlightsInventory.SearchFlightsByAirportOrCountryName(searchedListOfFlights, "ArrivalAirport", arrivalAirport, false);
                    FlightsInventory.ShowFlights(searchedListOfFlights);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    ((List<string>)result[0]).Add("Arrival Airport");
                    result[1] = searchedListOfFlights;
                    return result;

                // Search By Flight Class
                case "7":
                    int selectedClass;
                    bool validInput = int.TryParse(SearchBy("Flight Class", MenuOfFlightClasses()), out selectedClass);
                    if (validInput)
                    {
                        searchedListOfFlights = FlightsInventory.SearchFlightsByClass(searchedListOfFlights, (int)selectedClass);
                        FlightsInventory.ShowFlights(searchedListOfFlights, selectedClass);
                        Console.WriteLine();
                        Console.Write("Press Enter to continue");
                        Console.ReadLine();
                        ((List<string>)result[0]).Add("Flight Class");
                        result[1] = searchedListOfFlights;
                        return result;
                    }
                    else
                    {
                        return result;
                    }


                // clear search
                case "8":
                    searchedListOfFlights = FlightsInventory.Flights;
                    result[1] = searchedListOfFlights;
                    result[0] = new List<string>();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    return result;

                // Search ready - Select a Flight
                case "9":
                    // Pending functionality

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    return [];
                //Going back
                case "0":
                    Console.WriteLine();
                    Console.Write("Press Enter to Go back");
                    Console.ReadLine();
                    return [];

                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    return [];

            }
        }

        public static void ShowAndLaunchSearchFlightsMenu()
        {
            List<string> menu = SearchFlightsOptions();
            string title = "Please select a parameter to search flights";
            string searchFlight;
            object[] resultSearch = new object[2];
            List<string> filters = new List<string>();
            List<Flight> searchedListOfFlights = FlightsInventory.Flights;
            resultSearch[0] = filters;
            resultSearch[1] = searchedListOfFlights;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"*********************************");
                Console.WriteLine($"******** Current Filters ********");
                Console.WriteLine($"*********************************");
                Utilities.ShowListOfStrings((List<string>)resultSearch[0]);
                Console.WriteLine();
                searchFlight = Utilities.ShowMenu(menu, title);
                resultSearch = LaunchSearchFlightsSelection(searchFlight, (List<string>)resultSearch[0], (List<Flight>)resultSearch[1]);
            } while (searchFlight != "0");
        }


        // ******************************************
        // Search By
        // ******************************************

        private static string SearchBy(string by, List<string>? internalMenu = null)
        {
            string? searchTerm;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Search by {by} ******");
                if (internalMenu == null)
                {
                    Console.Write($"Please write the {by}: ");
                    searchTerm = Console.ReadLine();
                    Console.WriteLine($"Your search term: {searchTerm}");
                }
                else
                {
                    searchTerm = Utilities.ShowMenu(internalMenu);
                }
                Console.WriteLine();
            } while (string.IsNullOrEmpty(searchTerm));
            return searchTerm;
        }

        private static List<string> MenuOfFlightClasses()
        {
            List<string> menu = [];
            FlightClass[] allFlightClasses = (FlightClass[])Enum.GetValues(typeof(FlightClass));
            foreach (FlightClass fc in allFlightClasses)
            {
                menu.Add(fc.ToString());
            }
            return menu;
        }
        private static List<double> SearchByPrice(string by)
        {
            List<double> priceRange = [];
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Search by {by} ******");
                for (int i = 0; i < 2; i++)
                {
                    Console.Write($"Please write the the {(i == 0 ? "lower" : "upper")} price: ");
                    bool valid = double.TryParse(Console.ReadLine(), out double aux);
                    priceRange.Add(aux);
                }
                Console.WriteLine($"Your search price range: {priceRange[0]} - {priceRange[1]}");
                Console.WriteLine();
            } while (priceRange.Count < 2);
            return priceRange;
        }

        private static List<int> SearchByDate(string by)
        {
            List<int> date = [];
            bool allValid = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Search by {by} ******");
                Console.Write($"Please write the the YEAR in format YYYY (Enter for Current Year): ");
                string? year = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(year))
                {
                    year = DateTime.Now.Year.ToString();
                }
                bool validYear = int.TryParse(year, out int numericYear);
                Console.Write($"Please write the the MONTH in numeric format MM (Enter for Current Month): ");
                string? month = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(month))
                {
                    month = DateTime.Now.Month.ToString();
                }
                bool validMonth = int.TryParse(month, out int numericMonth);

                Console.Write($"Please write the the DAY in numeric format DD (Enter for today): ");
                string? day = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(day))
                {
                    day = DateTime.Now.Day.ToString();
                }
                bool validDay = int.TryParse(day, out int numericDay);

                allValid = validYear && validMonth && validDay;

                if (allValid)
                {
                    date.Add(numericYear);
                    date.Add(numericMonth);
                    date.Add(numericDay);
                }

                Console.WriteLine($"Date for search: {date[0]} - {date[1]} - {date[2]}");
                Console.WriteLine();
            } while (!allValid);
            return date;
        }

    }
}
