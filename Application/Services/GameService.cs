using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;

public class GameService : IGameService
{
    private readonly IGameRepository _gameRepository;

    public GameService(IGameRepository gameRepository)
    {
        _gameRepository = gameRepository;
    }

    public async Task<IEnumerable<GameDTO>> GetAllGamesAsync()
    {
        var games = await _gameRepository.GetAllAsync();

        var gamesDto = games.Select(game => new GameDTO
        {
            Id = (int)game.Id,
            Title = game.Title ?? "",
            Price = game.Price,
            Description = game.Description ?? "",
            Genre = game.Genre ?? ""
        }).ToList();

        return gamesDto;
    }

    public async Task<GameDTO> GetGameByIdAsync(int id)
    {
        var game = await _gameRepository.GetBy(game => game.Id.Equals(id));

        if (game is null)
            throw new NotFoundException("Jogo não encontrado.");

        return new GameDTO
        {
            Id = (int)game.Id,
            Title = game.Title ?? "",
            Price = game.Price,
            Description = game.Description ?? "",
            Genre = game.Genre ?? ""
        };
    }

    public async Task<GameDTO> CreateGameAsync(CreateGameDTO model)
    {
        var existingGame = await _gameRepository.GetBy(g => g.Title.ToUpper() == model.Title.ToUpper());
        if (existingGame is not null)
            throw new BusinessException($"Jogo com o título '{model.Title}' já existe.");

        var game = new Game
        (
            model.Title,
            model.Price,
            model.Description,
            model.Genre
        );

        await _gameRepository.AddAsync(game);

        return new GameDTO
        {
            Id = (int)game.Id,
            Title = game.Title ?? "",
            Price = game.Price,
            Description = game.Description ?? "",
            Genre = game.Genre ?? ""
        };
    }

    public async Task UpdateGameAsync(int id, UpdateGameDTO model)
    {
        var game = await _gameRepository.GetBy(g => g.Id.Equals(id));
        var ExistingGame = await _gameRepository.GetBy(g => g.Title.ToUpper() == model.Title.ToUpper() && g.Id != id);

        if (ExistingGame is not null)
            throw new BusinessException($"Jogo com o título '{model.Title}' já existe.");

        if (game == null)
            throw new NotFoundException($"Jogo {id} não encontrado.");

        if (!string.IsNullOrWhiteSpace(model.Title))
            game.Title = model.Title;

        if (model.Price.HasValue)
            game.Price = model.Price.Value;

        if (!string.IsNullOrWhiteSpace(model.Description))
            game.Description = model.Description;

        if (!string.IsNullOrEmpty(model.Genre))
            game.Genre = model.Genre;

        //validations
        Game.ValidateTitle(game.Title);
        Game.ValidateDescription(game.Description);
        Game.ValidateGenre(game.Genre);
        Game.ValidatePrice(game.Price);

        await _gameRepository.UpdateAsync(game);
    }

    public async Task DeleteGameAsync(int id)
    {
        var game = await _gameRepository.GetBy(g => g.Id.Equals(id));

        if (game == null)
            throw new NotFoundException($"Jogo {id} não encontrado.");

        await _gameRepository.DeleteAsync(id);
    }
}
