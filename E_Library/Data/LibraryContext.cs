using E_Library.Models;
using Microsoft.EntityFrameworkCore;

namespace E_Library.Data {
    public class LibraryContext : DbContext {

        public LibraryContext(DbContextOptions options) : base(options) { 
        
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Loan> Loans { get; set; }
        public DbSet<User> Users { get; set; }


        //protected override void OnModelCreating(ModelBuilder modelBuilder) {
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<Author>(entity =>
        //    {
        //        entity.HasKey(e => e.Id);
        //        entity.Property(e => e.Name).IsRequired();
        //        entity.Property(e => e.DateOfBirth);
        //    });

        //    modelBuilder.Entity<Author>().HasData(
        //        new Author {
        //            Id = 1,
        //            Name = "Stephen King",
        //            DateOfBirth = "21-03-1963"
        //        },
        //        new Author {
        //            Id = 2,
        //            Name = "Steven Erikson",
        //            DateOfBirth = "11-01-1967"
        //        }
        //    );
        //}

    }
}
