using Library.Management.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Library.Management.Infrastructure.DBContext;

public class LibraryDbContext : DbContext
{

   public LibraryDbContext(DbContextOptions<LibraryDbContext> options) : base(options)
   {
      
   }
   
   public DbSet<Book> Books { get; set; } 
   public DbSet<Author> Authors { get; set; }

   protected override void OnModelCreating(ModelBuilder modelBuilder)
   {
      base.OnModelCreating(modelBuilder);

      modelBuilder.Entity<Author>()
         .HasMany(a => a.Books)
         .WithOne()
         .HasForeignKey("AuthorId")
         .OnDelete(DeleteBehavior.Cascade);
   }
}