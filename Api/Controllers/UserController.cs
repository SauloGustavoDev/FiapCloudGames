using Application.DTOs;
using Application.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;
[Route("api/[controller]")]
[ApiController]
[Authorize]
public class UserController(IUserService userService) : ApiBaseController
{
    private readonly IUserService _userService = userService;
    [Authorize(Roles = "Admin")]
    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        return Success(user, "Usuário encontrado.");
    }
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> CreateUser(CreateUserDTO model)
    {
        var user = await _userService.CreateUserAsync(model);
        return CreatedResponse(user, "Usuário criado com sucesso.");
    }

    [Authorize(Roles = "Admin")]
    [HttpPatch("{id}")]
    public async Task<IActionResult> PartialUpdateUser(string id, UpdateUserDTO model)
    {
        await _userService.UpdateUserAsync(id, model);
        return Success("Usuário atualizado com sucesso.");
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(string id)
    {
        await _userService.DeleteUserAsync(id);
        return NoContent();
    }
}
