using AirportTicketBookingSystem.FlightManagement;
using AutoFixture.Xunit2;
using AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.Tests.ManagerFeaturesTests
{
    public class FilterBookingsTests
    {
        private readonly Fixture _fixture;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightsInventory _flightsInventory;
        private readonly IBookingRepository _bookingRepository;

        public FilterBookingsTests()
        {
            _fixture = new Fixture();
            _flightsInventory = new FlightsInventory();
            _passengerRepository = new PassengerRepository();
            _bookingRepository = new BookingRepository(_passengerRepository, _flightsInventory);
        }

        [Theory]
        [InlineAutoData]
        public void ShouldReturnBookingsWithFlightId(Passenger passenger)
        {
            //Arrange
            _passengerRepository.RegisteredPassengers.Add(passenger);

            var flight = _fixture.Build<Flight>().Create();
            var booking = _fixture.Build<Booking>()
                .With(b => b.Flight, flight)
                .With(b => b.Passenger, passenger)
                .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            // Act
            var filteredBookings = _bookingRepository.GetBookingsByFlightId(flight.Id);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Equal(filteredBookings[0].Flight, flight);
        }

        [Theory]
        [InlineAutoData(100, 200)]
        [InlineAutoData(150, 250)]
        [InlineAutoData(50, 150)]
        public void ShouldReturnBookingWithinPriceRange(double lowerSearchLimit, double upperSearchLimit, Passenger passenger)
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

            var booking = _fixture.Build<Booking>()
              .With(b => b.Flight, flight)
              .With(b => b.FlightAvailability, flightAvailabilityWithinRange)
              .With(b => b.Passenger, passenger)
              .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByPrice(lowerSearchLimit, upperSearchLimit);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.True(filteredBookings[0].FlightAvailability.Price >= lowerSearchLimit && filteredBookings[0].FlightAvailability.Price <= upperSearchLimit);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldReturnBookingWithDepartureCountry(string departureCountry, Passenger passenger)
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

            var booking = _fixture.Build<Booking>()
             .With(b => b.Flight, flight)
             .With(b => b.Passenger, passenger)
             .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByDepartureCountry(departureCountry);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Contains(departureCountry, filteredBookings[0].Flight.DepartureAirport.Country.Name);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldReturnBookingWithDestinationCountry(string destinationCountry, Passenger passenger)
        {
            //Arrange
            var country = _fixture.Build<Country>()
                .With(x => x.Name, destinationCountry)
                .Create();

            var airport = _fixture.Build<Airport>()
                .With(x => x.Country, country)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.ArrivalAirport, airport)
                .Create();

            var booking = _fixture.Build<Booking>()
             .With(b => b.Flight, flight)
             .With(b => b.Passenger, passenger)
             .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByDestinationCountry(destinationCountry);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Contains(destinationCountry, filteredBookings[0].Flight.ArrivalAirport.Country.Name);
        }

        [Theory]
        [InlineAutoData(2024, 12, 17)]
        [InlineAutoData(2023, 11, 30)]
        public void ShouldReturnBookingWithSpecificDepartureDate(int year, int month, int day, Passenger passenger)
        {
            //Arrange
            DateTime departureDate = new DateTime(year, month, day);
            var flight = _fixture.Build<Flight>()
                .With(x => x.DepartureDate, departureDate)
                .Create();

            var booking = _fixture.Build<Booking>()
           .With(b => b.Flight, flight)
           .With(b => b.Passenger, passenger)
           .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByDepartureDate([year, month, day]);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Equal(departureDate, filteredBookings[0].Flight.DepartureDate);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldReturnBookingWithDepartureAirport(string departureAirportName, Passenger passenger)
        {
            //Arrange
            var airport = _fixture.Build<Airport>()
                .With(x => x.Name, departureAirportName)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.DepartureAirport, airport)
                .Create();

            var booking = _fixture.Build<Booking>()
             .With(b => b.Flight, flight)
             .With(b => b.Passenger, passenger)
             .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByDepartureAirport(departureAirportName);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Contains(departureAirportName, filteredBookings[0].Flight.DepartureAirport.Name);
        }

        [Theory]
        [InlineAutoData("Colombia")]
        [InlineAutoData("Argentina")]
        [InlineAutoData()]
        public void ShouldReturnBookingWithArrivalAirport(string arrivalAirportName, Passenger passenger)
        {
            //Arrange
            var airport = _fixture.Build<Airport>()
                .With(x => x.Name, arrivalAirportName)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.ArrivalAirport, airport)
                .Create();

            var booking = _fixture.Build<Booking>()
             .With(b => b.Flight, flight)
             .With(b => b.Passenger, passenger)
             .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByArrivalAirport(arrivalAirportName);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Contains(arrivalAirportName, filteredBookings[0].Flight.ArrivalAirport.Name);
        }

        [Theory]
        [InlineAutoData]
        public void ShouldReturnBookingWithPassengerId(Passenger passenger)
        {
            //Arrange
            var booking = _fixture.Build<Booking>()
                .With(b => b.Passenger, passenger)
                .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByPassengerId(passenger.Id);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Equal(passenger.Id, filteredBookings[0].Passenger.Id);
        }

        [Theory]
        [InlineAutoData()]
        [InlineAutoData()]
        public void ShouldReturnBookingWithSpecificFlightClass(FlightClass flightClass, Passenger passenger)
        {
            //Arrange
            FlightAvailability flightAvailability = _fixture.Build<FlightAvailability>()
                            .With(fa => fa.FlightClass, flightClass)
                            .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.FlightAvailabilities, new List<FlightAvailability> { flightAvailability })
                .Create();

            var booking = _fixture.Build<Booking>()
                .With(b => b.FlightAvailability, flightAvailability)
                .With(b => b.Flight, flight)
                .With(b => b.Passenger, passenger)
                .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);

            var numberOfFlightClass = (int)flightClass;

            //Act
            var filteredBookings = _bookingRepository.GetBookingsByClass(numberOfFlightClass);

            //Assert
            Assert.Single(filteredBookings);
            Assert.Contains(booking, filteredBookings);
            Assert.Equal(flightClass, filteredBookings[0].FlightAvailability.FlightClass);
        }

    }
}
