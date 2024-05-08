using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AirportTicketBookingSystem.Users;
using AirportTicketBookingSystem.FlightManagement;

namespace AirportTicketBookingSystem.Utilities
{
    public static class ManageBookingsUtilities
    {
        public static void CancelBooking1(string bookingNumber, Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);

            if (bookings != null && validBookingNumber)
            {
                for (int i = 0; i < bookings.Count; i++)
                {
                    if (numericalBookingNumber == i + 1)
                    {
                        foreach (Passenger p in PassengerRepository.RegisteredPassengers)
                        {
                            if (p.Email.Equals(passenger.Email))
                            {
                                p.Bookings?.Remove(p.Bookings[i]);
                                break;
                            }
                        }
                        break;
                    }
                }
            }

        }

        public static void CancelBooking(string bookingNumber, Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);

            if (bookings != null && validBookingNumber)
            {

                if (BookingRepository.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)).Passenger.Email.Equals(passenger.Email))
                {
                    // booking belongs to passenger
                    // remove from booking repository and from passenger bookings
                    BookingRepository.Bookings.Remove(BookingRepository.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)));
                    foreach (Passenger p in PassengerRepository.RegisteredPassengers)
                    {
                        if (p.Email.Equals(passenger.Email))
                        {
                            p.Bookings?.Remove(p.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)));
                            Console.WriteLine();
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine($"Booking number {numericalBookingNumber} has been Canceled!");
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine();
                        }
                    }

                }
                else
                {
                    //booking does not belong to passenger
                    Console.WriteLine("The booking number you have selected does not exist in your bookings");
                }
            }
        }

        //public static void ModifyBooking1(string bookingNumber, Passenger passenger, Flight flight, FlightAvailability flightAvailability)
        //{
        //    List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
        //    bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);

        //    Booking booking = new(flight, flightAvailability, passenger);


        //    if (bookings != null && validBookingNumber)
        //    {
        //        for (int i = 0; i < bookings.Count; i++)
        //        {
        //            if (numericalBookingNumber == i + 1)
        //            {
        //                foreach (Passenger p in PassengerRepository.RegisteredPassengers)
        //                {
        //                    if (p.Email.Equals(passenger.Email))
        //                    {
        //                        p.Bookings?.Remove(p.Bookings[i]);
        //                        p.Bookings?.Add(booking);
        //                        break;
        //                    }
        //                }
        //                break;
        //            }
        //        }
        //    }
        //}

        public static void ModifyBooking(string bookingNumber, Passenger passenger, Flight flight, FlightAvailability flightAvailability)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);

            Booking booking = new(numericalBookingNumber, flight, flightAvailability, passenger);


            if (bookings != null && validBookingNumber)
            {
                if (BookingRepository.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)).Passenger.Email.Equals(passenger.Email))
                {
                    // remove and replace from booking repository and from passenger bookings
                    BookingRepository.Bookings.Remove(BookingRepository.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)));
                    BookingRepository.Bookings.Add(booking);
                    foreach (Passenger p in PassengerRepository.RegisteredPassengers)
                    {
                        if (p.Email.Equals(passenger.Email))
                        {
                            p.Bookings?.Remove(p.Bookings.Single(b => b.Id.Equals(numericalBookingNumber)));
                            p.Bookings?.Add(booking);
                            Console.WriteLine();
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine($"Booking number {numericalBookingNumber} has been Modified!");
                            Console.WriteLine("***********************************************************");
                            Console.WriteLine();
                        }
                    }

                }
                else
                {
                    //booking does not belong to passenger
                    Console.WriteLine("The booking number you have selected does not exist in your bookings");
                }
            }
        }

        public static void ViewMyBookings1(Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            Console.WriteLine();
            Console.WriteLine("**************************************");
            Console.WriteLine("**********  Your Bookings ************");
            Console.WriteLine("**************************************");
            Console.WriteLine();

            if (bookings == null)
            {
                Console.WriteLine("Currently you don't have bookings");
            }
            else
            {
                foreach (Booking booking in bookings)
                {
                    Console.WriteLine($"Booking number: {bookings.IndexOf(booking) + 1}");
                    Console.WriteLine(booking);
                }
            }
        }

        public static void ViewMyBookings(Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            Console.WriteLine();
            Console.WriteLine("**************************************");
            Console.WriteLine("**********  Your Bookings ************");
            Console.WriteLine("**************************************");
            Console.WriteLine();

            if (bookings == null)
            {
                Console.WriteLine("Currently you don't have bookings");
            }
            else
            {
                foreach (Booking booking in bookings)
                {
                    Console.WriteLine(booking);
                    Console.WriteLine();
                }
            }
        }

        public static Booking? SelectBooking(string bookingNumber, Passenger passenger)
        {
            List<Booking>? bookings = PassengerRepository.GetBookingsByPassenger(passenger);
            bool validBookingNumber = int.TryParse(bookingNumber, out int numericalBookingNumber);
            Booking? booking = null;

            if (bookings != null && validBookingNumber)
            {
                for (int i = 0; i < bookings.Count; i++)
                {
                    if (numericalBookingNumber == i + 1)
                    {
                        booking = bookings[i];
                        break;
                    }
                }
            }

            return booking;
        }


    }
}
