namespace Application.DTOs;
    public class GameDTO
    {
        public required int Id { get; set; }
        public required string Title { get; set; }
        public required decimal Price { get; set; }
        public required string Description { get; set; }
        public required string Genre { get; set; }
    }
