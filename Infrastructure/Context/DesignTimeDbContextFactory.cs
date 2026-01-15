using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Context;
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ContextDb>
    {
        public ContextDb CreateDbContext(string[] args)
        {
        var optionsBuilder = new DbContextOptionsBuilder<ContextDb>();

        optionsBuilder.UseSqlServer(
            "Data Source=.\\SQLExpress;Initial Catalog=FCG;Integrated Security=True;Encrypt=False;Trust Server Certificate=True"
        );

        return new ContextDb(optionsBuilder.Options);
    }
    }
