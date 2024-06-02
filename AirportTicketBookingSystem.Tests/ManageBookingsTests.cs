using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem;
using AutoFixture.Xunit2;
using System.Linq;
using AutoFixture;
using System.Collections.Generic;
using Xunit;
using System;
using AirportTicketBookingSystem.Users;

namespace AirportTicketBookingSystem.Tests
{
    public class ManageBookingsTests
    {
        private readonly Fixture _fixture;

        public ManageBookingsTests()
        {
            _fixture = new Fixture();

            //_fixture.Behaviors.OfType<ThrowingRecursionBehavior>().ToList()
            //  .ForEach(b => _fixture.Behaviors.Remove(b));

            //_fixture.Customize<Booking>(composer => composer.FromFactory(() =>
            //    new Booking(
            //        id: _fixture.Create<int>(),
            //        flight: _fixture.Create<Flight>(),
            //        flightAvailability: _fixture.Create<FlightAvailability>(),
            //        passenger: _fixture.Create<Passenger>()
            //    )));
            //_fixture.Behaviors.Add(new OmitOnRecursionBehavior());
        }

        [Fact]
        public void ShouldReturnCorrectBookingForPassenger()
        {
            //Arrange
            var passenger = _fixture.Build<Passenger>()
                .With(p => p.Id, _fixture.Create<int>)
                .Create();

            PassengerRepository.RegisteredPassengers.Clear();
            PassengerRepository.RegisteredPassengers.Add(passenger);

            var booking = _fixture.Build<Booking>()
                .With(b => b.Passenger, passenger)
                .Create();

            BookingRepository.Bookings.Add(booking);

            //Act
            var bookingsForPassenger = BookingRepository.GetBookingsByPassengerId(passenger.Id);

            //Assert
            Assert.Single(bookingsForPassenger);
            Assert.Single(BookingRepository.Bookings);
            Assert.Contains(booking, bookingsForPassenger);
            Assert.Contains(booking, BookingRepository.Bookings);

        }
    }
}