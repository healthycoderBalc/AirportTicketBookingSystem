using AirportTicketBookingSystem.CustomValidation;
using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Utilities.StorageUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AirportTicketBookingSystem.Utilities.LoadingUtilities
{
    public class StorageFlightsUtilities
    {
        private string directory = @"D:\Foothill\C#Course\Exercise\Storage\";
        private string flightsFileName = "flightData.csv";
        private string bookingFileName = "bookingsSaved.csv";

        private void CheckForExistingFlightFile()
        {
            string path = $"{directory}{flightsFileName}";

            bool existingFileFound = File.Exists(path);
            if (!existingFileFound)
            {
                //Create the directory
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(directory);

                //Create the empty file
                using FileStream fs = File.Create(path);
            }
        }

        public List<Flight> LoadFlightsFromFile()
        {
            List<Flight> flights = new List<Flight>();
            List<FlightAvailability> flightAvailabilities = new List<FlightAvailability>();

            string path = $"{directory}{flightsFileName}";
            List<string> errors = new List<string>();
            try
            {
                CheckForExistingFlightFile();

                string[] flightsAsString = File.ReadAllLines(path);
                for (int i = 0; i < flightsAsString.Length; i++)
                {
                    string[] flightSplits = flightsAsString[i].Split(';');

                    // flight Id
                    bool success = int.TryParse(flightSplits[0], out int flightId);
                    if (!success)
                    {
                        flightId = 0;
                    }

                    // Departure date
                    success = DateTime.TryParse(flightSplits[1], out DateTime departureDate);
                    if (!success)
                    {
                        errors.Add($"Review the DateTime format of the departure date in the line number {i + 1}. It should be YYYY, MM, DD, HH, MM, SS");
                    }

                    // Departure Airport
                    string departureAirportName = flightSplits[2];

                    // Departure Country
                    string departureCountryName = flightSplits[3];

                    // Arrival Airport
                    string arrivalAirportName = flightSplits[4];

                    // Destination Country
                    string destinationCountryName = flightSplits[5];

                    Country departureCountry = new Country(departureCountryName);
                    Airport departureAirport = new Airport(departureAirportName, departureCountry);

                    Country destinationCountry = new Country(destinationCountryName);
                    Airport arrivalAirport = new Airport(arrivalAirportName, destinationCountry);

                    bool correctlyLoaded;
                    for (int j = 0; j < 3; j++)
                    {
                        List<int> flightIndex = [6, 10, 14];
                        List<int> priceIndex = [7, 11, 15];
                        List<int> tPlaceIndex = [8, 12, 16];
                        List<int> aPlaceIndex = [9, 13, 17];
                        // FlightClass
                        string flightClass = flightSplits[flightIndex[j]];

                        // Price
                        success = double.TryParse(flightSplits[priceIndex[j]], out double price);
                        if (!success)
                        {
                            price = 0;//default value
                        }

                        // Total Places
                        success = int.TryParse(flightSplits[tPlaceIndex[j]], out int tPlaces);
                        if (!success)
                        {
                            tPlaces = 0;//default value
                        }

                        // Available Places
                        success = int.TryParse(flightSplits[aPlaceIndex[j]], out int aPlaces);
                        if (!success)
                        {
                            aPlaces = 0;//default value
                        }


                        //Create FlightAvailability
                        if (!string.IsNullOrEmpty(flightClass) && price != 0 && tPlaces != 0 & aPlaces != 0)
                        {
                            // if flight availability is provided
                            FlightAvailability flightAvailability = new FlightAvailability((FlightClass)Enum.Parse(typeof(FlightClass), flightClass), price, tPlaces, aPlaces);
                            bool faCorrectlyLoaded = ValidateCreationOfFlightAvailability(flightAvailability, i, j + 1);
                            if (faCorrectlyLoaded)
                            {
                                flightAvailabilities.Add(flightAvailability);
                            }
                        }
                        else if (j > 0)
                        {
                            // flight availability not provided but is not the first flight availability
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"The {j + 1} Flight Class and Price (FlightAvailability) was not provided.");
                            Console.ResetColor();
                        }
                        else
                        {
                            // flight availability not provided and is the first flight availability
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("The first Flight Class and Price (FlightAvailability) was not provided. The flight will not be loaded. It requires at least one flight availability");
                            Console.ResetColor();
                        }

                    }
                    if (flightAvailabilities.Count > 0)
                    {
                        Flight flight = new Flight(flightId, departureDate, departureAirport, arrivalAirport, flightAvailabilities);

                        correctlyLoaded = ValidateLoadOfFlights(flight, i + 1);
                        if (correctlyLoaded)
                        {
                            flights.Add(flight);
                        }
                    }
                    flightAvailabilities = [];
                }
            }

            catch (IndexOutOfRangeException iex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Something went wrong parsing the file, please check the data!");
                Console.WriteLine(iex.Message);
            }
            catch (FileNotFoundException fnfex)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("The file couldn't be found!");
                Console.WriteLine(fnfex.Message);
                Console.WriteLine(fnfex.StackTrace);
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Something went wrong while loading the file!");
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.ResetColor();
            }

            return flights;
        }

        public static bool ValidateLoadOfFlights(Flight flight, int fileLine)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(flight, null, null);
            Console.WriteLine($"Analizing FileLine Number {fileLine}");
            bool isValid = Validator.TryValidateObject(flight, context, results, true);
            if (!isValid)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                foreach (ValidationResult result in results)
                {
                    Console.WriteLine(result.ErrorMessage);
                }
                Console.ResetColor();
            }
            Console.WriteLine("Press enter to continue");
            Console.Read();
            return isValid;
        }

        public static bool ValidateCreationOfFlightAvailability(FlightAvailability flightAvailability, int fileLine, int flightAvailabilityNumber)
        {
            List<ValidationResult> results = new List<ValidationResult>();
            ValidationContext context = new ValidationContext(flightAvailability, null, null);
            Console.WriteLine($"Analizing FlightClass and Price ({flightAvailabilityNumber}) from FileLine Number {fileLine}");
            bool isValid = Validator.TryValidateObject(flightAvailability, context, results, true);
            if (!isValid)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                foreach (ValidationResult result in results)
                {
                    Console.WriteLine(result.ErrorMessage);
                }
                Console.ResetColor();
            }
            Console.WriteLine("Press enter to continue");
            Console.Read();
            return isValid;
        }

        public void FetchAnnotations2()
        {
            try
            {
                // Get the type
                Type type = typeof(Flight);
                // Get the properties associated with
                PropertyInfo[] properties = type.GetProperties();

                // Display the attributes for each of the properties
                for (int i = 0; i < properties.Length; i++)
                {
                    Object[] attributes = properties[i].GetCustomAttributes(true);
                    if (attributes.Length > 0)
                    {
                        Console.WriteLine();
                        Console.WriteLine($"Property: {properties[i].Name}");
                        if (properties[i].PropertyType == typeof(Int32))
                        {
                            Console.WriteLine($"    Type: Integer");
                        }
                        else if (properties[i].PropertyType == typeof(DateTime))
                        {
                            Console.WriteLine($"    Type: Date Time (YYYY-MM-DD HH:mm:ss)");

                        }
                        else if (properties[i].PropertyType == typeof(Airport))
                        {
                            Console.WriteLine($"    Type: Airport ");
                            Console.WriteLine($"        Airport fields: Name, Country");
                            Console.WriteLine($"            Name: Free Text ");
                            Console.WriteLine($"            Country fields: Name ");

                            Console.WriteLine($"                Name: Free Text ");
                        }
                        else if (properties[i].PropertyType == typeof(List<FlightAvailability>))
                        {
                            Console.WriteLine($"    Type: List of FlightAvailabitilies");
                            Console.WriteLine($"        FlightAvailabilities fields: FlightClass, Price, TotalPlaces, AvailablePlaces");
                            Console.WriteLine($"            FlightClass: Enum ");
                            Console.WriteLine($"            Price: Double ");
                            Console.WriteLine($"            TotalPlaces: Integer");
                            Console.WriteLine($"            AvailablePlaces: Integer");
                        }

                        Console.WriteLine($"    Constraints: ");
                        for (int j = 0; j < attributes.Length; j++)
                        {
                            if (attributes[j].GetType() == typeof(RequiredAttribute))
                            {
                                Console.WriteLine("\tRequired");
                            }
                            if (attributes[j].GetType() == typeof(KeyAttribute))
                            {
                                Console.WriteLine("\tMust be unique");

                            }
                            if (attributes[j].GetType() == typeof(DateMoreThanOrEqualToToday))
                            {
                                Console.WriteLine("\tDate range: today -> future");

                            }
                            if (attributes[j].GetType() == typeof(AtLeastOneElementInList))
                            {
                                Console.WriteLine("\tThere should be at least one element in the list of {0}", properties[i].Name);
                            }
                        }

                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception occurred: {0}", e.Message);
            }
        }


     


    }

}
