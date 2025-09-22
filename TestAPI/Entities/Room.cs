namespace TestAPI.Entities
{
    public class Room
    {
        public required int Id { get; set; }
        public required string Name { get; set; } = string.Empty;
        public required string Type { get; set; } = string.Empty;
        public required bool IsAvailable { get; set; } = false;
    }
}
