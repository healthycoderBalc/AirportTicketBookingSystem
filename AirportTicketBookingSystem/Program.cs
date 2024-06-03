using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.Utilities;
using AirportTicketBookingSystem.Utilities.LoadingUtilities;
using AirportTicketBookingSystem.Utilities.ManagerUtilities;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;
using AirportTicketBookingSystem.Utilities.UtilitiesInterfaces;

namespace AirportTicketBookingSystem
{
    public class Program
    {
        private readonly IUtilities _utilities;
        private readonly IManagerUtilities _managerUtilities;
        private readonly IPassengerUtilities _passengerUtilities;
        public Program(IUtilities utilities, IManagerUtilities managerUtilities, IPassengerUtilities passengerUtilities)
        {
            _utilities = utilities;
            _managerUtilities = managerUtilities;
            _passengerUtilities = passengerUtilities;
        }

        public static void Main(string[] args)
        {
            var passengerRepository = new PassengerRepository();
            var flightsInventory = new FlightsInventory();
            var bookingRepository = new BookingRepository(passengerRepository, flightsInventory);

            var filterByFlightUtilities = new FilterByFlightUtilities(flightsInventory);
            var filterByPassengerUtilities = new FilterByPassengerUtilities(passengerRepository);


            var utilities = new Utilities.Utilities();
            var filterByFlightClassUtilities = new FilterByFlightClassUtilities(utilities);
            var filterBookingsUtilities = new FilterBookingsUtilities(
                bookingRepository,
                filterByFlightUtilities,
                filterByPassengerUtilities,
                filterByFlightClassUtilities,
                utilities
                );

            var storageFlightUtilities = new StorageFlightsUtilities();
            var managerUtilities = new ManagerUtilities(
                filterBookingsUtilities, 
                flightsInventory, 
                passengerRepository, 
                bookingRepository,
                storageFlightUtilities,
                utilities
                );

            var manageBookingUtilities = new ManageBookingsUtilities(bookingRepository, passengerRepository);
            var bookFlightUtilities = new BookFlightUtilities(
                passengerRepository, 
                flightsInventory, 
                bookingRepository, 
                manageBookingUtilities,
                utilities
                );
            var searchFlightUtilities = new SearchFlightUtilities(flightsInventory, utilities, bookFlightUtilities);
            var passengerUtilities = new PassengerUtilities(
                searchFlightUtilities, 
                manageBookingUtilities, 
                bookFlightUtilities, 
                flightsInventory, 
                passengerRepository,
                utilities
                );
            var program = new Program(utilities, managerUtilities, passengerUtilities);

            program.ShowAndLaunchUserTypeMenu();
        }

        private void ShowAndLaunchUserTypeMenu()
        {
            List<string> menu = _utilities.UserTypeOptions();

            string userTypeSelection = string.Empty;
            do
            {
                userTypeSelection = _utilities.ShowMenu(menu);
                LaunchUserTypeSelection(userTypeSelection);
            } while (userTypeSelection != "0");
        }

        private void LaunchUserTypeSelection(string selection)
        {
            switch (selection)
            {
                // Manager User
                case "1":
                    string code = _managerUtilities.RequestManagerCode();
                    bool validated = Manager.Validate(code);
                    if (validated)
                    {
                        _managerUtilities.ShowAndLaunchManagerOptionsMenu();
                    }
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Passenger User
                case "2":
                    _passengerUtilities.ShowAndLaunchManagePassengerMenu();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                //Exit Application
                case "0":
                    _utilities.ExitApplication();
                    Console.WriteLine();
                    Console.Write("Press Enter to exit");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    break;
            }
        }
    }
}
