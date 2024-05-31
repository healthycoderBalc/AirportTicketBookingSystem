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

            PassengerRepository.RegisteredPassengers.Clear();
            var passenger =  _fixture.Create<Passenger>();
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

            PassengerRepository.RegisteredPassengers.Clear();
            var unregisteredPassenger = _fixture.Create<Passenger>();

            //Act
            //Assert
            Assert.Empty(PassengerRepository.RegisteredPassengers);
            var exception = Assert.Throws<InvalidOperationException>(
                () => PassengerRepository.CreateBooking(flight, unregisteredPassenger, flightAvailability));
            Assert.Equal("Passenger is not registered.", exception.Message);
        }
    }
}