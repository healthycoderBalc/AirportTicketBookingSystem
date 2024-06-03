using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.Utilities.UtilitiesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.PassengerUtilities
{
    public class BookFlightUtilities
    {
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightsInventory _flightsInventory;
        private readonly IBookingRepository _bookingRepository;
        private readonly ManageBookingsUtilities _manageBookingsUtilities;
        private readonly IUtilities _utilities;


        public BookFlightUtilities(IPassengerRepository passengerRepository, IFlightsInventory flightsInventory, IBookingRepository bookingRepository, ManageBookingsUtilities manageBookingsUtilities, IUtilities utilities)
        {
            _passengerRepository = passengerRepository; 
            _flightsInventory = flightsInventory;
            _bookingRepository = bookingRepository;
            _manageBookingsUtilities = manageBookingsUtilities;
            _utilities = utilities;
        }

        // ******************************************
        // Account
        // ******************************************

        public static List<string> PassengerAccountOptions()
        {
            List<string> menu = ["Create my account", "Already have account"];
            return menu;
        }

        private void LaunchAccountSelection(string selection, List<Flight> flightsOption)
        {
            switch (selection)
            {
                // Create Account
                case "1":
                    //create account
                    List<string> passengerData = CreateAccount("Creating Account");
                    Passenger createdPassengerAccount = _passengerRepository.CreateAccount(passengerData[0], passengerData[1], passengerData[2]);

                    MakeCompleteBooking(flightsOption, createdPassengerAccount, false);

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Have Account
                case "2":
                    List<string> passengerValidationData = ValidateAccount("Validating Account");
                    Passenger? validatedPassengerAccount = _passengerRepository.ValidateAccount(passengerValidationData[0], passengerValidationData[1]);
                    if (validatedPassengerAccount != null)
                    {
                        Console.WriteLine("Passenger validated: " + validatedPassengerAccount);
                        MakeCompleteBooking(flightsOption, validatedPassengerAccount, false);
                    }
                    else
                    {
                        Console.WriteLine("Passenger not found or multiple passengers found with the same credentials. Going back");
                        break;
                    }

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;


                //Going back
                case "0":
                    Console.WriteLine();
                    Console.Write("Press Enter to Go back");
                    Console.ReadLine();
                    break;


                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    break;


            }
        }

        public void ShowAndLaunchAccountMenu(List<Flight> flightsOption)
        {
            List<string> menu = PassengerAccountOptions();
            string title = "Please select an option to continue booking a flight";
            string accountOption;
            do
            {
                Console.WriteLine();
                accountOption = _utilities.ShowMenu(menu, title);
                LaunchAccountSelection(accountOption, flightsOption);
            } while (accountOption != "0");
        }


        // ******************************************
        // Account
        // ******************************************

        private static List<string> CreateAccount(string option)
        {
            List<string> passengerData = [];
            string? name = string.Empty;
            string? email = string.Empty; ;
            string? password = string.Empty; ;
            bool allFilled = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Now: {option} ******");
                if (string.IsNullOrEmpty(name))
                {
                    Console.Write($"Please write your name: ");
                    name = Console.ReadLine();
                }
                if (string.IsNullOrEmpty(email))
                {
                    Console.Write($"Please write your email: ");
                    email = Console.ReadLine();
                }
                if (string.IsNullOrEmpty(password))
                {
                    Console.Write($"Please write your password: ");
                    password = Console.ReadLine();
                }

                if (!string.IsNullOrEmpty(name) && !string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    allFilled = true;
                    passengerData.Add(name);
                    passengerData.Add(email);
                    passengerData.Add(password);
                }

            } while (!allFilled);

            Console.WriteLine($"Thank you {name}");
            Console.WriteLine();
            return passengerData;
        }

        public static List<string> ValidateAccount(string option)
        {
            List<string> passengerData = [];
            string? email = string.Empty; ;
            string? password = string.Empty; ;
            bool allFilled = false;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Now: {option} ******");
                if (string.IsNullOrEmpty(email))
                {
                    Console.Write($"Please write your email: ");
                    email = Console.ReadLine();
                }
                if (string.IsNullOrEmpty(password))
                {
                    Console.Write($"Please write your password: ");
                    password = Console.ReadLine();
                }

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(password))
                {
                    allFilled = true;
                    passengerData.Add(email);
                    passengerData.Add(password);
                }

            } while (!allFilled);

            Console.WriteLine();
            return passengerData;
        }


        // ******************************************
        // Booking
        // ******************************************

        private Flight SelectingFlightToBook(List<Flight> flightsOption)
        {
            Console.WriteLine();
            FlightsInventory.ShowFlights(flightsOption);
            bool validFlightNumber = false;
            Flight? flight = null;
            do
            {
                Console.Write("Please write the number (Id) of the flight you want to book: ");
                string? flightNumber = Console.ReadLine();
                if (flightNumber != null)
                {
                    validFlightNumber = int.TryParse(flightNumber, out int numericalFlightNumber);
                    if (validFlightNumber)
                    {
                        flight = _flightsInventory.GetFlightById(numericalFlightNumber);
                        if (flight != null)
                        {
                            Console.WriteLine();
                            Console.WriteLine("**************************************");
                            Console.WriteLine("This is the flight you have selected: ");
                            Console.WriteLine(Flight.ShowFlightShort(flight));
                            Console.WriteLine("**************************************");
                        }
                        else
                        {
                            Console.WriteLine("Select another flight");
                        }
                    }
                }
            } while (flight == null);
            return flight;
        }

        private FlightAvailability SelectingFlightAvailabilityToBook(Flight flight, Passenger passenger, bool modifyBooking, string? bookingNumberToModify = null)
        {
            Console.WriteLine();
            FlightAvailability? flightAvailability = null;
            do
            {
                string classSelected = _utilities.ShowMenu(SearchFlightUtilities.MenuOfFlightClasses(), "Now write the number of the Flight Class you want to book");
                if (classSelected != null)
                {
                    bool validClassNumber = int.TryParse(classSelected, out int numericalClassNumber);
                    if (validClassNumber)
                    {
                        flightAvailability = Flight.GetFlightAvailabilityByFlightClass(flight, (FlightClass)numericalClassNumber - 1);
                        if (flightAvailability != null)
                        {
                            // make the booking
                            if (modifyBooking && bookingNumberToModify != null)
                            {
                                _manageBookingsUtilities.ModifyBooking(bookingNumberToModify, passenger, flight, flightAvailability);
                            }
                            else
                            {
                                _bookingRepository.CreateBooking(flight, passenger, flightAvailability);

                            }
                        }
                        else
                        {
                            Console.WriteLine("Select another Flight class");
                        }
                    }
                }
            }
            while (flightAvailability == null);
            return flightAvailability;
        }

        public void MakeCompleteBooking(List<Flight> flightsOption, Passenger createdPassengerAccount, bool modifyBooking, string? bookingNumberToModify = null)
        {
            // select the flight
            Flight selectedFlight = SelectingFlightToBook(flightsOption);

            // select the class
            FlightAvailability flightAvailability = SelectingFlightAvailabilityToBook(selectedFlight, createdPassengerAccount, modifyBooking, bookingNumberToModify);

            Console.WriteLine("******************************************************");
            Console.WriteLine("*********You flight was booked successfully!**********");
            Console.WriteLine("******************************************************");

            Console.WriteLine(selectedFlight);
            Booking lastBooking = _bookingRepository.Bookings.Last(b => b.Passenger.Id.Equals(createdPassengerAccount.Id)) as Booking;
            Console.WriteLine(lastBooking);
        }
    }
}
