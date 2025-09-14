using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BookingService.Infrastructure.Persistence
{
    public class BookingDbContextFactory : IDesignTimeDbContextFactory<BookingDbContext>
    {
        public BookingDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<BookingDbContext>();

            optionsBuilder.UseSqlServer(
                "Server=localhost;Database=BookingDb;Trusted_Connection=True;TrustServerCertificate=True;");

            return new BookingDbContext(optionsBuilder.Options);
        }
    }
}
