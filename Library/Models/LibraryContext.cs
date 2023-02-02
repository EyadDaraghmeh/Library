using Microsoft.EntityFrameworkCore;

namespace Library.Models
{
    public class LibraryContext : DbContext
    {
        public LibraryContext(DbContextOptions<LibraryContext> options) : base(options) { }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<CustomersBooks> CustomersBooks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CustomersBooks>().HasKey(x => new { x.CustomersId, x.BooksId });
        }
    }
}
