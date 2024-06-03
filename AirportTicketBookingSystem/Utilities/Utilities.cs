using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.Utilities.ManagerUtilities;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;
using AirportTicketBookingSystem.Utilities.UtilitiesInterfaces;

namespace AirportTicketBookingSystem.Utilities
{
    public class Utilities : IUtilities
    {

        public string ShowMenu(List<string> options, string? title = null)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(title)) { Console.WriteLine(title); }
            Console.WriteLine("*****************************************");
            Console.WriteLine("*********Please select an option*********");
            Console.WriteLine("*****************************************");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"* {i + 1} - {options[i]}".PadRight(40) + "*");
            }
            Console.WriteLine("* 0 - Exit                              *");
            Console.WriteLine("*****************************************");

            Console.Write("Your selection is: ");
            string? selection = Console.ReadLine();
            return selection != null ? selection : "0";
        }



        public List<string> UserTypeOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("I'm a Manager");
            menu.Add("I'm a Passenger");

            return menu;
        }


        public void ExitApplication()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("*  Thank you for you time  *");
            Console.WriteLine("****************************");
        }


        public void ShowListOfStrings(List<string> strings)
        {
            if (strings.Count > 0)
            {

                foreach (string element in strings)
                {
                    Console.Write($"| {element} |");
                }
            }
            else
            {
                Console.WriteLine("Nothing to show");
            }
        }


    }
}
