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
            ShowAndLaunchUserTypeMenu();
        }

        private static void ShowAndLaunchUserTypeMenu()
        {
          List<string> menu = Utilities.Utilities.UserTypeOptions();

            string userTypeSelection = string.Empty;
            do
            {
                userTypeSelection = Utilities.Utilities.ShowMenu(menu);
                Utilities.Utilities.LaunchUserTypeSelection(userTypeSelection);
            } while (userTypeSelection != "0");
        }
    }
}
