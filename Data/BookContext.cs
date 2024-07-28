using Microsoft.EntityFrameworkCore;
using SimpleWebApi.Entities;

namespace SimpleWebApi.Data;
public class BookContext : DbContext
{
    public BookContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
}