using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Domain.Interfaces;
    public interface IUserRepository
    {
    Task<User?> GetByUsernameAsync(string username);
    Task<bool> CheckPasswordAsync(User user, string password);
    Task<IList<string>> GetRolesAsync(User user);
    Task<IdentityResult> CreateUserAsync(User user, string password);
    Task<IdentityResult> AddToRoleAsync(User user, string role);
    Task<bool> RoleExistsAsync(string role);
    Task<User?> GetByIdAsync(string id);
    Task<IdentityResult> SetUserNameAsync(User user, string username);
    Task<IdentityResult> SetEmailAsync(User user, string email);
    Task<IdentityResult> UpdateAsync(User user);
    Task<IEnumerable<User>> GetAllAsync();
    Task<IdentityResult> DeleteAsync(User user);
    Task<bool> ExistsByEmailAsync(string email);
}

