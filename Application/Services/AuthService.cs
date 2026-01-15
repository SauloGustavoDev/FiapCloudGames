using Application.DTOs;
using Application.Services.Interfaces;
using Domain.Exceptions;
using Domain.Interfaces;
namespace Application.Services;
    public class AuthService(IUserRepository userRepository, JwtService jwtService) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly JwtService _jwtService = jwtService;

    public async Task<LoginSucessDTO> LoginAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username) ?? throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            var isValidPassword = await _userRepository.CheckPasswordAsync(user, password);

            if (!isValidPassword)
                throw new UnauthorizedAccessException("Usuário ou senha inválidos");

            var roles = await _userRepository.GetRolesAsync(user);

            if (roles.Count != 1)
                throw new BusinessException("Usuário deve possuir exatamente uma role");

            var role = roles.Single();

            var token = _jwtService.GenerateToken(user, role);

            return new LoginSucessDTO(token,user, role);
        }
    }
    

