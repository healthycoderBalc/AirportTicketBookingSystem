using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem;
using AutoFixture;
using AutoFixture.Xunit2;
using System.Linq;
using Xunit;
using System;

namespace AirportTicketBookingSystem.Tests
{
    public class SearchFlightsTests
    {
        public SearchFlightsTests()
        {

        }

        [Theory]
        [InlineAutoData(100, 200)]
        [InlineAutoData(150, 250)]
        [InlineAutoData(50, 150)]
        public void ShouldGetAFlightIfIsIncludedInThePriceRange(double lowerSearchLimit, double upperSearchLimit)
        {
            //Arrange
            var fixture = new Fixture();
            var random = new Random();

            var flightAvailabilityWithinRange = fixture.Build<FlightAvailability>()
                .With(x => x.Price, random.NextDouble() * (upperSearchLimit - lowerSearchLimit) + lowerSearchLimit)
                .Create();

            var flight = fixture.Build<Flight>()
                .With(x => x.FlightAvailabilities, new List<FlightAvailability> { flightAvailabilityWithinRange })
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByPrice(flights, new List<double> { lowerSearchLimit, upperSearchLimit });

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(flight.FlightAvailabilities, fa => fa.Price >= lowerSearchLimit && fa.Price <= upperSearchLimit);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldGetAFlightIfItsDepartureCountryIs(string departureCountry)
        {
            //Arrange
            var fixture = new Fixture();

            var country = fixture.Build<Country>()
                .With(x => x.Name, departureCountry )
                .Create();

            var airport = fixture.Build<Airport>()
                .With(x => x.Country, country)
                .Create();

            var flight = fixture.Build<Flight>()
                .With(x => x.DepartureAirport, airport)
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByCountryName(flights, departureCountry, true);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(departureCountry, flight.DepartureAirport.Country.Name);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldGetAFlightIfItsArrivalCountryIs(string arrivalCountry)
        {
            //Arrange
            var fixture = new Fixture();

            var country = fixture.Build<Country>()
                .With(x => x.Name, arrivalCountry)
                .Create();

            var airport = fixture.Build<Airport>()
                .With(x => x.Country, country)
                .Create();

            var flight = fixture.Build<Flight>()
                .With(x => x.ArrivalAirport, airport)
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByCountryName(flights, arrivalCountry, false);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(arrivalCountry, flight.ArrivalAirport.Country.Name);
        }


    }
}