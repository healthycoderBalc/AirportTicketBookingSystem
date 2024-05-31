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
        private readonly Fixture _fixture;

        public SearchFlightsTests()
        {
            _fixture = new Fixture();
        }

        [Theory]
        [InlineAutoData(100, 200)]
        [InlineAutoData(150, 250)]
        [InlineAutoData(50, 150)]
        public void ShouldReturnFlightWithinPriceRange(double lowerSearchLimit, double upperSearchLimit)
        {
            //Arrange
            var random = new Random();
            double price = random.NextDouble() * (upperSearchLimit - lowerSearchLimit) + lowerSearchLimit;

            var flightAvailabilityWithinRange = _fixture.Build<FlightAvailability>()
                .With(x => x.Price, price)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.FlightAvailabilities, new List<FlightAvailability> { flightAvailabilityWithinRange })
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByPrice(flights, new List<double> { lowerSearchLimit, upperSearchLimit });

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(filteredFlights[0].FlightAvailabilities, fa => fa.Price >= lowerSearchLimit && fa.Price <= upperSearchLimit);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldReturnFlightWithDepartureCountry(string departureCountry)
        {
            //Arrange
            var country = _fixture.Build<Country>()
                .With(x => x.Name, departureCountry)
                .Create();

            var airport = _fixture.Build<Airport>()
                .With(x => x.Country, country)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.DepartureAirport, airport)
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByCountryName(flights, departureCountry, true);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(departureCountry, filteredFlights[0].DepartureAirport.Country.Name);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldReturnFlightWithArrivalCountry(string arrivalCountry)
        {
            //Arrange
            var country = _fixture.Build<Country>()
                .With(x => x.Name, arrivalCountry)
                .Create();

            var airport = _fixture.Build<Airport>()
                .With(x => x.Country, country)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.ArrivalAirport, airport)
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByCountryName(flights, arrivalCountry, false);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(arrivalCountry, filteredFlights[0].ArrivalAirport.Country.Name);
        }

        [Theory]
        [InlineAutoData("El Dorado")]
        [InlineAutoData("Ezeiza")]
        [InlineAutoData()]
        public void ShouldReturnFlightWithDepartureAirport(string departureAirportName)
        {
            //Arrange
            var airport = _fixture.Build<Airport>()
                .With(x => x.Name, departureAirportName)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.DepartureAirport, airport)
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByAirportName(flights, departureAirportName, true);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(departureAirportName, filteredFlights[0].DepartureAirport.Name);
        }

        [Theory]
        [InlineAutoData("El Dorado")]
        [InlineAutoData("Ezeiza")]
        [InlineAutoData()]
        public void ShouldReturnFlightWithArrivalAirport(string arrivalAirportName)
        {
            //Arrange
            var airport = _fixture.Build<Airport>()
                .With(x => x.Name, arrivalAirportName)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.ArrivalAirport, airport)
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByAirportName(flights, arrivalAirportName, false);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Contains(arrivalAirportName, filteredFlights[0].ArrivalAirport.Name);
        }


        [Theory]
        [InlineAutoData(2024, 12, 17)]
        [InlineAutoData(2023, 11, 30)]
        public void ShouldReturnFlightWithSpecificDepartureDate(int year, int month, int day)
        {
            //Arrange
            DateTime departureDate = new DateTime(year, month, day);
            var flight = _fixture.Build<Flight>()
                .With(x => x.DepartureDate, departureDate)
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByDate(flights, [year, month, day]);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Equal(departureDate, filteredFlights[0].DepartureDate);
        }

        [Theory]
        [InlineAutoData()]
        [InlineAutoData()]
        public void ShouldReturnFlightWithSpecificFlightClass(FlightClass flightClass)
        {
            //Arrange
            FlightAvailability flightAvailability = _fixture.Build<FlightAvailability>()
                            .With(fa => fa.FlightClass, flightClass)
                            .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.FlightAvailabilities, new List<FlightAvailability> { flightAvailability })
                .Create();

            var flights = new List<Flight> { flight };

            //Act
            var filteredFlights = FlightsInventory.SearchFlightsByClass(flights, (int)flightClass);

            //Assert
            Assert.Single(filteredFlights);
            Assert.Contains(flight, filteredFlights);
            Assert.Equal(flightClass, filteredFlights[0].FlightAvailabilities[0].FlightClass);
        }

    }
}