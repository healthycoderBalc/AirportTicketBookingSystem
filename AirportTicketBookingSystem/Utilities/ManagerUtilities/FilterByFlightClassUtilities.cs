using AirportTicketBookingSystem.FlightManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class FilterByFlightClassUtilities
    {
        public static int GetFlightClassSelected()
        {
            Console.WriteLine();
            int numericalFlightClass;
            bool validFlightClass = false;
            do
            {
                List<string> menu = MenuOfFlightClasses();
                string flightClass = Utilities.ShowMenu(menu, "Now write the number (Id) of the Flight Class you want to filter by");
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
