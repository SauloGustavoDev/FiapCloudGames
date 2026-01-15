using Domain.Entities;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace Tests.Infrastructure.Context;
    public class ContextTest
    {
    [Fact]
    public void CanSaveUserWithEmail()
    {
        var options = new DbContextOptionsBuilder<ContextDb>()
            .UseInMemoryDatabase(databaseName: "TestDB")
            .Options;

        using (var context = new ContextDb(options))
        {
            var user = new User("Test", "valid@fiap.com.br");
            context.Users.Add(user);
            context.SaveChanges();
        }

        using (var context = new ContextDb(options))
        {
            Assert.Equal(1, context.Users.Count());
        }
    }
}


