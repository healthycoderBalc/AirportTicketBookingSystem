using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.PassengerUtilities
{
    public static class BookFlightUtilities
    {
        // ******************************************
        // Account
        // ******************************************

        public static List<string> PassengerAccountOptions()
        {
            List<string> menu = ["Create my account", "Already have account"];
            return menu;
        }

        private static void LaunchAccountSelection(string selection, List<Flight> flightsOption)
        {
            switch (selection)
            {
                // Create Account
                case "1":
                    //create account
                    List<string> passengerData = CreateAccount("Creating Account");
                    Passenger createdPassengerAccount = PassengerRepository.CreateAccount(passengerData[0], passengerData[1], passengerData[2]);

                    MakeCompleteBooking(flightsOption, createdPassengerAccount, false);

                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Have Account
                case "2":
                    List<string> passengerValidationData = ValidateAccount("Validating Account");
                    Passenger? validatedPassengerAccount = PassengerRepository.ValidateAccount(passengerValidationData[0], passengerValidationData[1]);
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

        public static void ShowAndLaunchAccountMenu(List<Flight> flightsOption)
        {
            List<string> menu = PassengerAccountOptions();
            string title = "Please select an option to continue booking a flight";
            string accountOption;
            do
            {
                Console.WriteLine();
                accountOption = Utilities.ShowMenu(menu, title);
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

        private static Flight SelectingFlightToBook(List<Flight> flightsOption)
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
                        flight = FlightsInventory.GetFlightById(numericalFlightNumber);
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

        private static FlightAvailability SelectingFlightAvailabilityToBook(Flight flight, Passenger passenger, bool modifyBooking, string? bookingNumberToModify = null)
        {
            Console.WriteLine();
            FlightAvailability? flightAvailability = null;
            do
            {
                string classSelected = Utilities.ShowMenu(SearchFlightUtilities.MenuOfFlightClasses(), "Now write the number of the Flight Class you want to book");
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
                                ManageBookingsUtilities.ModifyBooking(bookingNumberToModify, passenger, flight, flightAvailability);
                            }
                            else
                            {
                                PassengerRepository.CreateBooking(flight, passenger, flightAvailability);

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

        public static void MakeCompleteBooking(List<Flight> flightsOption, Passenger createdPassengerAccount, bool modifyBooking, string? bookingNumberToModify = null)
        {
            // select the flight
            Flight selectedFlight = SelectingFlightToBook(flightsOption);

            // select the class
            FlightAvailability flightAvailability = SelectingFlightAvailabilityToBook(selectedFlight, createdPassengerAccount, modifyBooking, bookingNumberToModify);

            Console.WriteLine("******************************************************");
            Console.WriteLine("*********You flight was booked successfully!**********");
            Console.WriteLine("******************************************************");

            Console.WriteLine(selectedFlight);
            Console.WriteLine(createdPassengerAccount?.Bookings?.Last());
        }
    }
}
