using Domain.Enums;
using Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities;
public class User : IdentityUser
{
    public string Name { get; set; } = string.Empty;
    public Role Role { get; set; } = Role.User;

    public User(string name, string email, Role role = Role.User)
    {
        ValidateName(name);
        var emailAddress = new Email(email);

        Name = name;
        Email = emailAddress.Address;
        UserName = emailAddress.Address;
        Role = role;
    }

    protected User() { }
    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Nome não pode ser vazio ou nulo");
    }
}