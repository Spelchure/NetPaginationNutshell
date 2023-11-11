using Microsoft.EntityFrameworkCore;
using PaginationExample.Model;

namespace PaginationExample.Database;

public class ItemDbContext : DbContext
{
   public DbSet<Item> Items { get; set; }

   public ItemDbContext(DbContextOptions<ItemDbContext> options) : base(options)
   {
      Database.EnsureCreated();
   }

   protected override void OnModelCreating(ModelBuilder builder)
   {
      var items = Enumerable.Range(1, 100).Select(index => new Item
      {
         Id = index,
         Name = $"Item at index = {index}"
      }).ToList();
      builder.Entity<Item>().HasData(items);
      base.OnModelCreating(builder);
   }
}