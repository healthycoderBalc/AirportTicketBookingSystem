using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Utilities.UtilitiesInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public class FilterByFlightClassUtilities
    {
        private readonly IUtilities _utilities;

        public FilterByFlightClassUtilities(IUtilities Utilities) {  _utilities = Utilities; }
        public int GetFlightClassSelected()
        {
            Console.WriteLine();
            int numericalFlightClass;
            bool validFlightClass = false;
            do
            {
                List<string> menu = MenuOfFlightClasses();
                string flightClass = _utilities.ShowMenu(menu, "Now write the number (Id) of the Flight Class you want to filter by");
                validFlightClass = int.TryParse(flightClass, out numericalFlightClass);
                if (!validFlightClass)
                {
                    Console.WriteLine("Invalid input. Select another flight class");
                }
            } while (!validFlightClass);
            return numericalFlightClass;
        }

        public static List<string> MenuOfFlightClasses()
        {
            List<string> menu = [];
            FlightClass[] allFlightClasses = (FlightClass[])Enum.GetValues(typeof(FlightClass));
            foreach (FlightClass fc in allFlightClasses)
            {
                menu.Add(fc.ToString());
            }
            return menu;
        }
    }
}
