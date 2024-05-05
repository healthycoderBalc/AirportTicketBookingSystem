using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Utilities;

namespace AirportTicketBookingSystem
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            ShowInitialMenu();
        }

        private static void ShowInitialMenu()
        {
            List<string> menu = new List<string>();
            menu.Add("I'm a Manager");
            menu.Add("I'm a Passenger");

            string selection = string.Empty;
            do
            {
                selection = Utilities.Utilities.ShowMenu(menu);
                Utilities.Utilities.LaunchInitialSelection(selection);
            } while (selection != "0");
            //return selection;
        }
    }
}
