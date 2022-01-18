using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookStore.Models
{
    public class BookStroeDbContext : DbContext
    {

        public BookStroeDbContext(DbContextOptions<BookStroeDbContext> options) : base(options)
        {

        }
        public DbSet<Author> Authors { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
