namespace AirportTicketBookingSystem.Users
{
    public interface IPassenger
    {
        string Email { get; set; }
        int Id { get; set; }
        string Name { get; set; }
        string Password { get; set; }
    }
}