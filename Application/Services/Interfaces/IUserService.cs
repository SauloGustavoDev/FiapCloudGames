using Application.DTOs;

namespace Application.Services.Interfaces;
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO> GetUserByIdAsync(string id);
        Task<UserDTO> CreateUserAsync(CreateUserDTO model);
        Task UpdateUserAsync(string id, UpdateUserDTO model);
        Task DeleteUserAsync(string id);
    }
