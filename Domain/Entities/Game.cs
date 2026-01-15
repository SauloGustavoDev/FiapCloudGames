using Domain.Exceptions;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities;
    public class Game
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public decimal Price { get; set; }
        public required string Description { get; set; }
        public required string Genre { get; set; }

    [SetsRequiredMembers]

    public Game(int id, string title, decimal price, string description, string genre)
            : this(title, price, description, genre)
        {
            Id = id;
        }
    [SetsRequiredMembers]

    public Game(string title, decimal price, string description, string genre)
        {
            ValidateTitle(title);
            ValidatePrice(price);
            ValidateDescription(description);
            ValidateGenre(genre);

            Title = title;
            Price = price;
            Description = description;
            Genre = genre;
        }

        public Game() { }


        public static void ValidateTitle(string? title)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new BusinessException("Título não pode ser vazio ou nulo.");
        }

        public static void ValidatePrice(decimal? price)
        {
            if (price is null or < 0)
                throw new BusinessException("Preço não pode ser negativo.");
        }

        public static void ValidateDescription(string? description)
        {
            if (string.IsNullOrWhiteSpace(description))
                throw new BusinessException("Descrição não pode ser vazio ou nulo.");
        }

        public static void ValidateGenre(string? genre)
        {
            if (string.IsNullOrWhiteSpace(genre))
                throw new BusinessException("Genero não pode ser vazio ou nulo.");
        }
    }
