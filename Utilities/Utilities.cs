﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.Utilities
{
    internal class Utilities
    {
        internal static string ShowMenu(List<string> options, string? title = null)
        {
            Console.WriteLine();
            if (!string.IsNullOrEmpty(title)) { Console.WriteLine(title); }
            Console.WriteLine("*****************************");
            Console.WriteLine("***Please select an option***");
            Console.WriteLine("*****************************");
            for (int i = 0; i < options.Count; i++)
            {
                Console.WriteLine($"* {i + 1} - {options[i]}".PadRight(28)+"*");
            }
            Console.WriteLine("* 0 - Exit                  *");
            Console.WriteLine("*****************************");

            Console.Write("Your selection is: ");
            string? selection = Console.ReadLine();
            return selection != null ? selection : "0";
        }

        internal static void LaunchUserTypeSelection(string selection)
        {
            switch (selection)
            {
                // Manager User
                case "1":
                    string code = ManagerUtilities.RequestManagerCode();
                    Manager.Validate(code);
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                // Passenger User
                case "2":
                    PassengerUtilities.ShowAndLaunchManagePassengerMenu();
                    Console.WriteLine();
                    Console.Write("Press Enter to continue");
                    Console.ReadLine();
                    break;

                //Exit Application
                case "0":
                    ExitApplication();
                    Console.WriteLine();
                    Console.Write("Press Enter to exit");
                    Console.ReadLine();
                    break;
                default:
                    Console.WriteLine("Yoy have not selected a valid option, please try again: ");
                    break;
            }
        }

        public static List<string> UserTypeOptions()
        {
            List<string> menu = new List<string>();
            menu.Add("I'm a Manager");
            menu.Add("I'm a Passenger");

            return menu;
        }


        public static void ExitApplication()
        {
            Console.WriteLine("****************************");
            Console.WriteLine("*  Thank you for you time  *");
            Console.WriteLine("****************************");
        }



    }
}
