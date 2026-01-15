using Domain.Entities;
using Domain.ValueObjects;
using Infrastructure.Context;
using Infrastructure.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Tests.Domain.Helper;
using Xunit;

namespace Tests.Infrastructure.Repository;
public class UserRepositoryTests
{
    private readonly ContextDb _context;
    private readonly UserRepository _repository;
    public UserRepositoryTests()
    {
            var services = new ServiceCollection();

            services.AddDbContext<ContextDb>(options =>
                options.UseInMemoryDatabase(Guid.NewGuid().ToString()));

            services.AddIdentityCore<User>(options =>
            {
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequiredLength = 6;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ContextDb>();

            var provider = services.BuildServiceProvider();

            var userManager = provider.GetRequiredService<UserManager<User>>();
            var roleManager = provider.GetRequiredService<RoleManager<IdentityRole>>();
            _context = provider.GetRequiredService<ContextDb>();

            _repository = new UserRepository(userManager, roleManager, _context);
        }

    [Fact]
    public async Task AddAsync_ValidUser_ShouldAddUserToDatabase()
    {
        //  Arrange
        var user = new User("José Silva", "emailteste@gmail.com");
        var password = new Password("Password123@");

        // Act
        await _repository.CreateUserAsync(user, password.Hash);

        // Assert
        var saveUser = await _context.Users.FirstOrDefaultAsync();
        Assert.NotNull(saveUser);
        Assert.Equal("emailteste@gmail.com", saveUser.Email!);
    }

    [Fact]
    public async Task DeleteAsync_ExistUser_ShouldDeleteUserFromDatabase()
    {
        // Arrange
        var user = new User("José Silva", "emailteste@gmail.com");
        var password = new Password("Password123@");

        // Act
        await _repository.CreateUserAsync(user, password.Hash);
        await _repository.DeleteAsync(user);

        // Assert
        Assert.Null(await _context.Users.FindAsync(user.Id));
    }

    [Fact]
    public async Task UpdateAsync_ExistUser_ShouldUpdateUser()
    {
        // Arrange
        var user = new User("José Silva", "emailteste@gmail.com");
        var password = new Password("Password123@");

        // Act
        await _repository.CreateUserAsync(user, password.Hash);
        user.Email = new Email("professor@gmail.com").ToString();
        await _repository.UpdateAsync(user);

        // Assert
        Assert.True(await _repository.ExistsByEmailAsync("professor@gmail.com"));
    }
}
