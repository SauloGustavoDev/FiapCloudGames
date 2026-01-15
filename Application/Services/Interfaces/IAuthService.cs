
using Application.DTOs;

namespace Application.Services.Interfaces;
    public interface IAuthService
    {
        Task<LoginSucessDTO> LoginAsync(string username, string password);
    }

