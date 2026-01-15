using Domain.ValueObjects;
using Xunit;

namespace Tests.Domain.ValueObjects;
public class EmailTests
{
    [Theory]
    [InlineData("professor@fiap.com.br")]
    [InlineData("usuario@pm3.com.br")]
    public void Constructor_ValidEmail_CreatesInstance(string validEmail)
    {
        var email = new Email(validEmail);
        Assert.Equal(validEmail, email.Address);
    }

    [Theory]
    [InlineData("emailErrado.com")]
    [InlineData("semarroba")]
    public void Constructor_InvalidEmail_ThrowsArgumentException(string invalidEmail)
    {
        Assert.Throws<ArgumentException>(() => new Email(invalidEmail));
    }
}