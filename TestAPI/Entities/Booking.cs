namespace TestAPI.Entities
{
    public class Booking
    {
        public required int Id { get; set; }
        public required string GuestName { get; set; } = string.Empty;
        public required int RoomId { get; set; } = 0;
        public required DateTime CheckInDate { get; set; }
        public required DateTime CheckOutDate { get; set; }
    }
}