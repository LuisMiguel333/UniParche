using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace UniParche.DataAccess.DbContext;

public class UniParcheDbContextFactory : IDesignTimeDbContextFactory<UniParcheDbContext>
{
    public UniParcheDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<UniParcheDbContext>();
        optionsBuilder.UseSqlServer("Server=(local);Database=UniParche;Trusted_Connection=true;TrustServerCertificate=true;");

        return new UniParcheDbContext(optionsBuilder.Options);
    }
}
