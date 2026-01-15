using Application.DTOs;
namespace Application.Services.Interfaces;
    public interface IGameService
    {
        Task<IEnumerable<GameDTO>> GetAllGamesAsync();
        Task<GameDTO> GetGameByIdAsync(int id);
        Task<GameDTO> CreateGameAsync(CreateGameDTO model);
        Task UpdateGameAsync(int id, UpdateGameDTO model);
        Task DeleteGameAsync(int id);
    }
