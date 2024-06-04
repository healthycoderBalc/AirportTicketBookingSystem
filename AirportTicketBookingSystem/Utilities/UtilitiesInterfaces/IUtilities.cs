using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.UtilitiesInterfaces
{
    public interface IUtilities
    {
        string ShowMenu(List<string> options, string? title = null);
        List<string> UserTypeOptions();
        void ExitApplication();
        public void ShowListOfStrings(List<string> strings);
    }
}
