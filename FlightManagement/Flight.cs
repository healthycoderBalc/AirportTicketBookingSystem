﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.FlightManagement
{
    public class Flight
    {
        public int Id { get; set; }
        public DateTime DepartureDate { get; set; }
        public Airport DepartureAirport { get; set; }
        public Airport ArrivalAirport { get; set; }
        public List<FlightAvailability> FlightAvailabilities { get; set; }

        public Flight(int id, DateTime departureDate, Airport departureAirport, Airport arrivalAirport, List<FlightAvailability> flightAvailabilities)
        {
            Id = id;
            DepartureDate = departureDate;
            DepartureAirport = departureAirport;
            ArrivalAirport = arrivalAirport;
            FlightAvailabilities = flightAvailabilities;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Flight Id: {Id}");
            stringBuilder.AppendLine("Departure:");
            stringBuilder.AppendLine($"  Airport: {DepartureAirport}");
            stringBuilder.AppendLine($"  Date: {DepartureDate}");
            stringBuilder.AppendLine("Arrival:");
            stringBuilder.AppendLine($"  Airport: {ArrivalAirport}");

            return stringBuilder.ToString();
        }


        public static string ShowFlightShort(Flight flight)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"{flight.Id}. ");
            stringBuilder.Append($"From: {flight.DepartureAirport.Name} - {flight.DepartureAirport.Country.Name}");
            stringBuilder.Append($" | To: {flight.ArrivalAirport.Name} - {flight.ArrivalAirport.Country.Name}");
            stringBuilder.Append($" | Date: {flight.DepartureDate}");

            return stringBuilder.ToString();
        }

        public static string ShowFlightAvailabilities(List<FlightAvailability> flightAvailabilities, int? flightClass = null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine($"Classes:");
            foreach (FlightAvailability flightAvailability in flightAvailabilities)
            {
                if (flightAvailability.AvailablePlaces > 0)
                {
                    if (flightClass != null && flightAvailability.FlightClass == (FlightClass)flightClass - 1)
                    {
                        stringBuilder.AppendLine($"  {flightAvailability.FlightClass}: ${flightAvailability.Price} - Places: {flightAvailability.TotalPlaces}");
                    }
                    else if (flightClass == null)
                    {
                        stringBuilder.AppendLine($"  {flightAvailability.FlightClass}: ${flightAvailability.Price} - Places: {flightAvailability.TotalPlaces}");
                    }
                }
            }
            return stringBuilder.ToString();
        }

        public static string ShowFlightAvailabilitiesShort(List<FlightAvailability> flightAvailabilities, int? flightClass = null)
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append($"Classes:");
            foreach (FlightAvailability flightAvailability in flightAvailabilities)
            {
                if (flightAvailability.AvailablePlaces > 0)
                {
                    if (flightClass != null && flightAvailability.FlightClass == (FlightClass)flightClass - 1)
                    {
                        stringBuilder.Append($"  {flightAvailability.FlightClass.ToString()[0]}: ${flightAvailability.Price} - Places: {flightAvailability.TotalPlaces}");
                    }
                    else if (flightClass == null)
                    {
                        stringBuilder.Append($"  {flightAvailability.FlightClass.ToString()[0]}: ${flightAvailability.Price} - Places: {flightAvailability.TotalPlaces}");
                    }
                }
            }
            return stringBuilder.ToString();
        }
    }
}
