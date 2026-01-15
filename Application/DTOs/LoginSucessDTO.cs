
using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.DTOs;
    public class LoginSucessDTO
    {
        public required string Token { get; set; }
        public required UserDTO User { get; set; }

    [SetsRequiredMembers]
    public LoginSucessDTO(string token, User user, string role)
    {
        Token = token;
        User = new UserDTO(user, role);
    }
}

