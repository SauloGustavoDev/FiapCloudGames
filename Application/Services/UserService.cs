using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.ValueObjects;

namespace Application.Services;
public class UserService(IUserRepository userRepository) : IUserService
{
    private readonly IUserRepository _userRepository = userRepository;
    public async Task<UserDTO> CreateUserAsync(CreateUserDTO model)
    {
        var exists = await _userRepository.GetByUsernameAsync(model.Email);

        if (exists is not null)
            throw new BusinessException("Já existe um usuário com este email.");

        var user = new User(model.Name, model.Email);
        var password = new Password(model.Password);
        
        if (!await _userRepository.RoleExistsAsync(model.Role))
                throw new BusinessException($"Role '{model.Role}' não encontrada.");

        var result = await _userRepository.CreateUserAsync(user, password.PlainText);
        if (!result.Succeeded)
            throw new BusinessException("Erro ao criar usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));

        await _userRepository.AddToRoleAsync(user, model.Role);

        return new UserDTO(user, model.Role);
    }

    public async Task DeleteUserAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("Usuário não encontrado.");

        var result = await _userRepository.DeleteAsync(user);
        if (!result.Succeeded)
            throw new BusinessException("Erro ao excluir usuário: " + string.Join(", ", result.Errors.Select(e => e.Description)));
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
    {
        var users = await _userRepository.GetAllAsync();

        var userDtos = new List<UserDTO>();

        foreach (var user in users)
        {
            var roles = await _userRepository.GetRolesAsync(user);
            var role = roles.Single();

            userDtos.Add(new UserDTO(user, role));
        }

        return userDtos;
    }


    public async Task<UserDTO> GetUserByIdAsync(string id)
    {
        var user = await _userRepository.GetByIdAsync(id) ?? throw new NotFoundException("Usuário não encontrado.");

        var roles = await _userRepository.GetRolesAsync(user);

        if (roles.Count != 1)
            throw new BusinessException("Usuário deve possuir exatamente uma role");

        var role = roles.Single();

        return new UserDTO(user, role);
    }

    public async Task UpdateUserAsync(string id, UpdateUserDTO userUpdate)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user is null)
            throw new NotFoundException("Usuário não encontrado.");

        if (!string.IsNullOrWhiteSpace(userUpdate.Name))
            user.Name = userUpdate.Name;

        if (!string.IsNullOrWhiteSpace(userUpdate.Email))
        {
            var emailAddress = new Email(userUpdate.Email);

            var setEmailResult = await _userRepository.SetEmailAsync(user, emailAddress.Address!);
            if (!setEmailResult.Succeeded)
                throw new BusinessException("Erro ao atualizar email: " + string.Join(", ", setEmailResult.Errors.Select(e => e.Description)));

            var setUserNameResult = await _userRepository.SetUserNameAsync(user, emailAddress.Address!);
            if (!setUserNameResult.Succeeded)
                throw new BusinessException("Erro ao atualizar username: " + string.Join(", ", setUserNameResult.Errors.Select(e => e.Description)));
        }

        var updateResult = await _userRepository.UpdateAsync(user);
        if (!updateResult.Succeeded)
            throw new BusinessException("Erro ao atualizar usuário: " + string.Join(", ", updateResult.Errors.Select(e => e.Description)));
    }
}
