using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Context;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repository
{
    public class UserRepository(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, ContextDb context) : IUserRepository
    {
        private readonly UserManager<User> _userManager = userManager;
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly ContextDb _context = context;

        public async Task<User?> GetByUsernameAsync(string username)
        => await _userManager.FindByNameAsync(username);
        public async Task<IdentityResult> CreateUserAsync(User user, string password)
        => await _userManager.CreateAsync(user, password);
        public async Task<bool> CheckPasswordAsync(User user, string password)
        => await _userManager.CheckPasswordAsync(user, password);
        public async Task<IList<string>> GetRolesAsync(User user)
        => await _userManager.GetRolesAsync(user);
        public async Task<bool> RoleExistsAsync(string role)
        => await _roleManager.RoleExistsAsync(role);
        public async Task<IdentityResult> AddToRoleAsync(User user, string role)
        => await _userManager.AddToRoleAsync(user, role);
        public async Task<User?> GetByIdAsync(string id)
        =>  await _userManager.FindByIdAsync(id);
        public async Task<IdentityResult> SetEmailAsync(User user, string email)
        => await _userManager.SetEmailAsync(user, email);
        public async Task<IdentityResult> SetUserNameAsync(User user, string username)
        => await _userManager.SetUserNameAsync(user, username);
        public async Task<IdentityResult> UpdateAsync(User user)
        => await _userManager.UpdateAsync(user);
        public async Task<IEnumerable<User>> GetAllAsync()
        => await _userManager.Users.ToListAsync();
        public async Task<IdentityResult> DeleteAsync(User user)
        => await _userManager.DeleteAsync(user);
        public async Task<bool> ExistsByEmailAsync(string email)
        => await _context.Users.AnyAsync(u => u.Email == email);

    }
}
