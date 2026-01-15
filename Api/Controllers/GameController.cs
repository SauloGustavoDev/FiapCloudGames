using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class GameController : ApiBaseController
    {
        private readonly IGameService _gameService;

        public GameController(IGameService gameService)
        {
            _gameService = gameService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGames()
        {
            var games = await _gameService.GetAllGamesAsync();

            return Success(games, "Lista de jogos retornada com sucesso.");
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetGame(int id)
        {
            var game = await _gameService.GetGameByIdAsync(id);

            return Success(game, "Jogo encontrado.");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateGame(CreateGameDTO model)
        {
            var game = await _gameService.CreateGameAsync(model);
            return CreatedResponse(game, "Jogo criado com sucesso.");
        }


        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> PartialUpdateGame(int id, UpdateGameDTO model)
        {
            await _gameService.UpdateGameAsync(id, model);
            return Success("Jogo atualizado com sucesso!");
        }


        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteGame([FromRoute] int id)
        {

            await _gameService.DeleteGameAsync(id);
            return NoContent();
        }
    }
}

