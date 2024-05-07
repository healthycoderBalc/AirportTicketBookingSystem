using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities
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
                    List<string> passengerData = CreateAccount("Creating Account");
                    Passenger createdPassengerAccount = PassengerRepository.CreateAccount(passengerData[0], passengerData[1], passengerData[2]);
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
                    } else
                    {
                        Console.WriteLine("Passenger not found or multiple passengers found with the same credentials.");
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
        // Search By
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

        private static List<string> ValidateAccount(string option)
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


    }
}
