using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.ManagerUtilities
{
    public static class FilterByPriceUtilities
    {
        public static List<double> GetSelectedPriceRange()
        {
            List<double> priceRange = [];
            do
            {
                Console.WriteLine();
                Console.WriteLine($"****** Filter By Price ******");
                for (int i = 0; i < 2; i++)
                {
                    Console.Write($"Please write the the {(i == 0 ? "lower" : "upper")} price: ");
                    bool valid = double.TryParse(Console.ReadLine(), out double aux);
                    priceRange.Add(aux);
                }
                Console.WriteLine($"Your filter price range: {priceRange[0]} - {priceRange[1]}");
                Console.WriteLine();
            } while (priceRange.Count < 2);
            return priceRange;
        }
    }
}
