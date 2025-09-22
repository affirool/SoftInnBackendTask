namespace TestAPI.DTO
{
    public class AddBookingDTO
    {
        public required string GuestName { get; set; } = string.Empty;
        public required int RoomId { get; set; } = 0;
        public required DateTime CheckInDate { get; set; }
        public required DateTime CheckOutDate { get; set; }
    }
}
