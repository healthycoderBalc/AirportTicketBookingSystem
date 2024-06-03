using AirportTicketBookingSystem.FlightManagement;
using AirportTicketBookingSystem.Utilities.PassengerUtilities;
using AirportTicketBookingSystem;
using AutoFixture.Xunit2;
using System.Linq;
using AutoFixture;
using System.Collections.Generic;
using Xunit;
using System;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.RepositoryInterfaces;

namespace AirportTicketBookingSystem.Tests
{
    public class ManageBookingsTests
    {
        private readonly Fixture _fixture;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightsInventory _flightsInventory;
        private readonly ManageBookingsUtilities _manageBookingsUtilities;

        public ManageBookingsTests()
        {
            _fixture = new Fixture();
            // _fixture.Customize<IPassenger>(composer => composer.FromFactory(() => _fixture.Create<Passenger>()));
            // CleanupRepositories();
            _flightsInventory = new FlightsInventory();
            _passengerRepository = new PassengerRepository();
            _bookingRepository = new BookingRepository(_passengerRepository, _flightsInventory);
            _manageBookingsUtilities = new ManageBookingsUtilities(_bookingRepository, _passengerRepository);
        }

        //private static void CleanupRepositories()
        //{
        //    PassengerRepository.RegisteredPassengers.Clear();
        //    BookingRepository.Bookings.Clear();
        //    FlightsInventory.Flights.Clear();
        //}

        //public void Dispose() { CleanupRepositories(); }

        [Fact]
        public void ShouldReturnCorrectBookingForPassenger()
        {
            //Arrange
            var passenger = _fixture.Build<Passenger>()
                .With(p => p.Id, _fixture.Create<int>)
                .Create();

            _passengerRepository.RegisteredPassengers.Add(passenger);

            var booking = _fixture.Build<Booking>()
                .With(b => b.Passenger, passenger)
                .Create();

            _bookingRepository.Bookings.Add(booking);

            //Act
            var bookingsForPassenger = _bookingRepository.GetBookingsByPassengerId(passenger.Id);

            //Assert
            Assert.Single(bookingsForPassenger);
            Assert.Single(_bookingRepository.Bookings);
            Assert.Contains(booking, bookingsForPassenger);
            Assert.Contains(booking, _bookingRepository.Bookings);

        }

        [Fact]
        public void ShouldCorrectlyCancelABooking()
        {
            // Arrange
            var passenger = _fixture.Create<Passenger>();
            var booking = _fixture.Build<Booking>()
                .With(b => b.Passenger, passenger)
                .Create();
            //booking.Passenger = passenger;
            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);
            Assert.Contains(booking, _bookingRepository.Bookings);

            //Act
            _manageBookingsUtilities.CancelBooking(booking.Id.ToString(), passenger);

            //Assert
            Assert.Empty(_bookingRepository.Bookings);

        }

        [Theory]
        [InlineAutoData("Maria", "maria@email.com", "thisIsThePassword")]
        public void ShouldCorrectlyUpdateABooking(string name, string email, string password, int id)
        {
            //Arrange
            var passenger = _fixture.Build<Passenger>()
                .With(p => p.Name, name)
                .With(p => p.Email, email)
                .With(p => p.Password, password)
                .With(p => p.Id, id).Create();

            _passengerRepository.RegisteredPassengers.Clear();
            _passengerRepository.RegisteredPassengers.Add(passenger);

            var flightAvailability = _fixture.Build<FlightAvailability>().Create();
            var flight = _fixture.Build<Flight>()
                .With(f => f.FlightAvailabilities, new List<FlightAvailability>() { flightAvailability })
                .Create();

            _flightsInventory.Flights.Add(flight);

            var booking = _fixture.Build<Booking>()
                .With(b => b.Passenger, passenger)
                .With(b => b.Flight, flight)
                .With(b => b.FlightAvailability, flightAvailability)
                .Create();

            _bookingRepository.Bookings.Clear();
            _bookingRepository.Bookings.Add(booking);
            Assert.Contains(booking, _bookingRepository.Bookings);


            var newFlightAvailability = _fixture.Build<FlightAvailability>()
                .Create();
            var newFlight = _fixture.Build<Flight>()
                .With(f => f.FlightAvailabilities, new List<FlightAvailability> { newFlightAvailability })
                .Create();

            //Act
            _manageBookingsUtilities.ModifyBooking(booking.Id.ToString(), passenger, newFlight, newFlightAvailability);

            //Assert
            Assert.Single(_bookingRepository.Bookings);
            //var updatedBooking = BookingRepository.Bookings[0];
            //Assert.Equal(booking.Id, updatedBooking.Id);
            //Assert.Equal(booking.Passenger, updatedBooking.Passenger);
            //Assert.Equal(newFlight,updatedBooking.Flight);
            //Assert.Equal(newFlightAvailability, updatedBooking.FlightAvailability);


        }
    }
}