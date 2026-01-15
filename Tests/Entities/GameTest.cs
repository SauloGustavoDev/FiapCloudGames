using Domain.Entities;
using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Tests.Domain.Entities;
public class GameTests
{
    [Fact]
    public void Constructor_ValidTitle_CreatesGame()
    {
        //Arrange / Act
        var game = new Game("Test Game", 0.99m, "descricao", "terror");

        // Assert
        Assert.Equal("Test Game", game.Title);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void Constructor_InvalidTitle_ThrowsBusinessErrorDetailsException(string invalidTitle)
    {
        Assert.Throws<BusinessException>(() => new Game(invalidTitle, 0.99m, "descricao", "terror"));
    }

    [Fact]
    public void Constructor_ValidPrice_CreatesGame()
    {
        // Arrange & Act
        var game = new Game("Test Game", 0.99m, "descricao", "terror");

        // Assert
        Assert.Equal(0.99m, game.Price);
    }
}
