using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class FilterByDateUtilities
    {
        public static List<int> GetDates()
        {
            List<int> date = [];
            bool allValid;
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Search by Date ******");
                Console.Write($"Please write the the YEAR in format YYYY (Enter for Current Year): ");
                string? year = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(year))
                {
                    year = DateTime.Now.Year.ToString();
                }
                bool validYear = int.TryParse(year, out int numericYear);
                Console.Write($"Please write the the MONTH in numeric format MM (Enter for Current Month): ");
                string? month = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(month))
                {
                    month = DateTime.Now.Month.ToString();
                }
                bool validMonth = int.TryParse(month, out int numericMonth);

                Console.Write($"Please write the the DAY in numeric format DD (Enter for today): ");
                string? day = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(day))
                {
                    day = DateTime.Now.Day.ToString();
                }
                bool validDay = int.TryParse(day, out int numericDay);

                allValid = validYear && validMonth && validDay;

                if (allValid)
                {
                    date.Add(numericYear);
                    date.Add(numericMonth);
                    date.Add(numericDay);
                }

                Console.WriteLine($"Date for filter: {date[0]} - {date[1]} - {date[2]}");
                Console.WriteLine();
            } while (!allValid);
            return date;
        }
    }
}
