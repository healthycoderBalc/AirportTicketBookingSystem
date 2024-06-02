using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem;
using AutoFixture;
using AutoFixture.Xunit2;
using System.Linq;
using Xunit;
using System;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.Tests
{
    public class BookingCreationTests
    {
        private readonly Fixture _fixture;

        public BookingCreationTests()
        {
            _fixture = new Fixture();

            _fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
              .ForEach(b => _fixture.Behaviors.Remove(b));
            _fixture.Behaviors.Add(new OmitOnRecursionBehavior());

            _fixture.Customize<Passenger>(composer => composer.FromFactory(() =>
                new Passenger(
                    id: _fixture.Create<int>(),
                    name: _fixture.Create<string>(),
                    email: _fixture.Create<string>(),
                    password: _fixture.Create<string>()
                )));
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

            FlightsInventory.Flights.Add(flight);

            PassengerRepository.RegisteredPassengers.Clear();
            var passenger = _fixture.Create<Passenger>();
            PassengerRepository.RegisteredPassengers.Add(passenger);

            passenger.Bookings?.Clear();

            //Act
            var bookingCreated = PassengerRepository.CreateBooking(flight, passenger, flightAvailability);

            //Assert
            List<Booking>? bookings = passenger.Bookings;
            Assert.Single(bookings);
            Assert.True(PassengerRepository.RegisteredPassengers.Count > 0);
            Assert.True(passenger?.Bookings?.Count > 0);
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

            FlightsInventory.Flights.Add(flight);

            PassengerRepository.RegisteredPassengers.Clear();
            var unregisteredPassenger = _fixture.Create<Passenger>();

            //Act
            //Assert
            Assert.Empty(PassengerRepository.RegisteredPassengers);
            var exception = Assert.Throws<InvalidOperationException>(
                () => PassengerRepository.CreateBooking(flight, unregisteredPassenger, flightAvailability));
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

            FlightsInventory.Flights.Clear();

            PassengerRepository.RegisteredPassengers.Clear();
            var passenger = _fixture.Create<Passenger>();
            PassengerRepository.RegisteredPassengers.Add(passenger);

            passenger.Bookings?.Clear();

            //Act
            //Assert
            Assert.Empty(FlightsInventory.Flights);
            var exception = Assert.Throws<InvalidOperationException>(
                () => PassengerRepository.CreateBooking(invalidFlight, passenger, flightAvailability));
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

            FlightsInventory.Flights.Add(flight);


            PassengerRepository.RegisteredPassengers.Clear();
            var passenger = _fixture.Create<Passenger>();
            PassengerRepository.RegisteredPassengers.Add(passenger);

            passenger.Bookings?.Clear();

            //Act
            //Assert
            var exception = Assert.Throws<InvalidOperationException>(
                () => PassengerRepository.CreateBooking(flight, passenger, flightAvailability));
            Assert.Equal("That Flight Class does not exist in the selected flight.", exception.Message);
        }

    }
}