using Domain.Entities;
using System.Diagnostics.CodeAnalysis;

namespace Application.DTOs;
    public class UserDTO
    {
    public required string Id { get; set; }
    public required string Name { get; set; }
    public required string Role { get; set; }
    [SetsRequiredMembers]
    public UserDTO(User user, string role)
    {
        Id = user.Id;
        Name = user.Name;
        Role = role;
    }
}

