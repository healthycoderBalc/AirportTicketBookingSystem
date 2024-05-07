﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.FlightManagement;
using System.Linq;
using System.Reflection;
using System.Diagnostics.Metrics;

namespace AirportTicketBookingSystem
{
    public static class FlightsInventory
    {
        public static List<Flight> Flights = new List<Flight>();
        public static List<Country> Countries = new List<Country>();
        public static List<Airport> Airports = new List<Airport>();

        // ******************************************
        // Load Data
        // ******************************************

        public static void LoadFlightsData()
        {
            //Flights.Clear();
            LoadCountries();
            LoadAirports();
            LoadFlights();
            LoadFlightAvailability();
        }

        public static void LoadCountries()
        {
            List<string> countryNames = ["Argentina", "Colombia", "Chile"];

            foreach (string countryName in countryNames)
            {
                Countries.Add(new Country(countryName));
            }
        }

        public static void LoadAirports()
        {
            List<string> ArgentinaAirportNames = ["Aeroparque", "Ezeiza", "Olaya Herrera"];
            List<string> ColombiaAirportNames = ["El Dorado", "Puente Aereo", "Ing. Ambrosio"];
            List<string> ChileAirportNames = ["Santiago", "Araucania", "Arturo Merino Benitez"];

            foreach (Country country in Countries)
            {
                if (country.Name.Equals("Argentina"))
                {
                    foreach (string airportName in ArgentinaAirportNames)
                    {
                        Airports.Add(new Airport(airportName, country));
                    }

                }
                else if (country.Name.Equals("Colombia"))
                {
                    foreach (string airportName in ColombiaAirportNames)
                    {
                        Airports.Add(new Airport(airportName, country));
                    }
                }
                else if (country.Name.Equals("Chile"))
                {
                    foreach (string airportName in ChileAirportNames)
                    {
                        Airports.Add(new Airport(airportName, country));
                    }
                }
            }
        }

        public static void LoadFlights()
        {
            List<FlightAvailability> flightAvailabilities = LoadFlightAvailability();
            Flight f1 = new(1, new DateTime(2024, 5, 6, 10, 30, 0), Airports[0], Airports[2], flightAvailabilities);
            Flight f2 = new(2, new DateTime(2024, 5, 7, 10, 30, 0), Airports[1], Airports[3], flightAvailabilities);
            Flight f3 = new(3, new DateTime(2024, 5, 8, 10, 30, 0), Airports[2], Airports[4], flightAvailabilities);
            Flight f4 = new(4, new DateTime(2024, 5, 9, 10, 30, 0), Airports[3], Airports[5], flightAvailabilities);
            Flight f5 = new(5, new DateTime(2024, 5, 6, 22, 30, 0), Airports[4], Airports[6], flightAvailabilities);
            Flight f6 = new(6, new DateTime(2024, 5, 7, 15, 30, 0), Airports[5], Airports[7], flightAvailabilities);
            Flight f7 = new(7, new DateTime(2024, 5, 8, 20, 30, 0), Airports[6], Airports[8], flightAvailabilities);
            Flight f8 = new(8, new DateTime(2024, 5, 9, 18, 30, 0), Airports[7], Airports[0], flightAvailabilities);
            Flight f9 = new(9, new DateTime(2024, 5, 6, 10, 0, 0), Airports[8], Airports[1], flightAvailabilities);
            Flight f10 = new(10, new DateTime(2024, 5, 7, 7, 30, 0), Airports[0], Airports[2], flightAvailabilities);

            Flights.AddRange([f1, f2, f3, f4, f5, f6, f7, f8, f9, f10]);

        }

        public static List<FlightAvailability> LoadFlightAvailability()
        {
            List<int> placesByClass = [20, 10, 5];
            List<double> pricesByClass = [1000, 1500, 2000];
            List<FlightAvailability> flightsByClass = new List<FlightAvailability>();
            foreach (FlightClass fc in Enum.GetValues(typeof(FlightClass)))
            {
                int places = placesByClass[(int)fc];
                double price = pricesByClass[(int)fc];
                flightsByClass.Add(new FlightAvailability(fc, price, places, places));
            }
            return flightsByClass;
        }

        // ******************************************
        // Searches
        // ******************************************

         public static List<Flight> SearchFlightsByAirportOrCountryName(List<Flight> flightsToSearch, string fieldName, string fieldValue, bool country)
        {
            // getting type of flight and property by its name
            Type flightType = typeof(Flight);
            PropertyInfo? property = flightType.GetProperty(fieldName);

            if (property != null)
            {
                IEnumerable<Flight> resultSearch =
                   flightsToSearch.Where(flight =>
                   { // using property to check search
                       var propertyValue = property.GetValue(flight);
                       if (propertyValue != null && propertyValue.GetType() == typeof(Airport))
                       {
                           if (country)
                           {
                               return ((Airport)propertyValue).Country.Name == fieldValue;
                           }
                           else
                           {
                               return ((Airport)propertyValue).Name == fieldValue;
                           }
                       }
                       return false;
                   });
                return resultSearch.ToList();
            }
            else
            {
                return [];
            }
        }


        public static List<Flight> SearchFlightsByClass(List<Flight> flightsToSearch, int fcNumber)
        {

            IEnumerable<Flight> resultSearch =
               flightsToSearch.Where(flight => flight.FlightAvailabilities.Any(availability => ((int)availability.FlightClass == fcNumber && availability.AvailablePlaces >0)));

            return resultSearch.ToList();
        }

        public static List<Flight> SearchFlightsByPrice(List<Flight> flightsToSearch, List<double> priceRange)
        {
            IEnumerable<Flight> resultSearch =
              flightsToSearch.Where(flight => flight.FlightAvailabilities.Any(availability => (availability.Price >= priceRange[0] && availability.Price <= priceRange[1])));

            return resultSearch.ToList();
        }

        public static void ShowFlights(List<Flight> flights, int? selectedClass = null)
        {
            if (flights.Count > 0)
            {
                Console.WriteLine();
                Console.WriteLine($"*********************************");
                Console.WriteLine($"********* Flights Found *********");
                Console.WriteLine($"*********************************");
                foreach (Flight f in flights)
                {
                    Console.WriteLine();
                    Console.WriteLine(f);
                    Console.WriteLine(Flight.ShowFlightAvailabilities(f.FlightAvailabilities, selectedClass ?? null));
                }
                Console.WriteLine($"*********************************");
            }
            else { Console.WriteLine("\nNo flights to show"); }
        }

    }
}
