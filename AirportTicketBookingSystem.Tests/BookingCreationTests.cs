using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem;
using AutoFixture;
using AutoFixture.Xunit2;
using System.Linq;
using Xunit;
using System;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.RepositoryInterfaces;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;

namespace AirportTicketBookingSystem.Tests
{
    public class BookingCreationTests
    {
        private readonly Fixture _fixture;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightsInventory _flightsInventory;
        private readonly ManageBookingsUtilities _manageBookingsUtilities;

        public BookingCreationTests()
        {
            _fixture = new Fixture();
            _passengerRepository = new PassengerRepository();
            _flightsInventory = new FlightsInventory();
            _bookingRepository = new BookingRepository(_passengerRepository, _flightsInventory);
            _manageBookingsUtilities = new ManageBookingsUtilities(_bookingRepository, _passengerRepository);

        }

        [Fact]
        public void ShouldCreateBookingIfPassengerRegistered()
        {
            //Arrange
            var flightAvailability = _fixture.Build<FlightAvailability>()
                .Create();

            var flightAvailabilities = new List<FlightAvailability> { flightAvailability };

            var flight = _fixture.Build<Flight>()
                .With(f => f.FlightAvailabilities, flightAvailabilities)
                .Create();
            _flightsInventory.Flights.Add(flight);

            var passenger = _fixture.Create<Passenger>();
            _passengerRepository.RegisteredPassengers.Add(passenger);

            //Act
            var bookingCreated = _bookingRepository.CreateBooking(flight, passenger, flightAvailability);

            //Assert
            Assert.Single(_bookingRepository.Bookings);
            Assert.Single(_passengerRepository.RegisteredPassengers);
            Assert.Equal(passenger, _bookingRepository.Bookings[0].Passenger);

        }

        [Fact]
        public void ShouldThrowExeptionIfPassengerNotRegistered()
        {
            //Arrange
            var flightAvailability = _fixture.Build<FlightAvailability>()
                .Create();

            var flightAvailabilities = new List<FlightAvailability> { flightAvailability };

            var flight = _fixture.Build<Flight>()
                .With(f => f.FlightAvailabilities, flightAvailabilities)
                .Create();

            _flightsInventory.Flights.Add(flight);

            var unregisteredPassenger = _fixture.Create<Passenger>();

            //Act
            //Assert
            Assert.Empty(_passengerRepository.RegisteredPassengers);
            var exception = Assert.Throws<InvalidOperationException>(
                () => _bookingRepository.CreateBooking(flight, unregisteredPassenger, flightAvailability));
            Assert.Equal("Passenger is not registered.", exception.Message);

        }


        [Fact]
        public void ShouldThrowExeptionIfInvalidFlight()
        {
            //Arrange
            var flightAvailability = _fixture.Build<FlightAvailability>()
                .Create();

            var flightAvailabilities = new List<FlightAvailability> { flightAvailability };

            var invalidFlight = _fixture.Build<Flight>()
                .With(f => f.FlightAvailabilities, flightAvailabilities)
                .Create();

            var passenger = _fixture.Create<Passenger>();
            _passengerRepository.RegisteredPassengers.Add(passenger);

            //Act
            //Assert
            Assert.Empty(_flightsInventory.Flights);
            var exception = Assert.Throws<InvalidOperationException>(
                () => _bookingRepository.CreateBooking(invalidFlight, passenger, flightAvailability));
            Assert.Equal("Flight does not exist.", exception.Message);

        }

        [Theory]
        [InlineAutoData(FlightClass.Economy)]
        [InlineAutoData(FlightClass.Business)]
        [InlineAutoData(FlightClass.FirstClass)]
        public void ShouldThrowExeptionIfInvalidFlightClass(FlightClass flightClass)
        {
            //Arrange
            var flightAvailability = _fixture.Build<FlightAvailability>()
                .With(x => x.FlightClass, flightClass)
                .Create();

            var differentFlightClass = Enum.GetValues(typeof(FlightClass))
                .Cast<FlightClass>()
                .First(fc => fc != flightClass);

            var differentFlightAvailability = _fixture.Build<FlightAvailability>()
                .With(x => x.FlightClass, differentFlightClass)
                .Create();

            var flight = _fixture.Build<Flight>()
                .With(x => x.FlightAvailabilities, new List<FlightAvailability> { differentFlightAvailability })
                .Create();

            _flightsInventory.Flights.Add(flight);


            var passenger = _fixture.Build<Passenger>()
                .Create();
            _passengerRepository.RegisteredPassengers.Add(passenger);


            //Act
            //Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => _bookingRepository.CreateBooking(flight, passenger, flightAvailability));
            Assert.Equal("That Flight Class does not exist in the selected flight.", exception.Message);

        }

    }
}