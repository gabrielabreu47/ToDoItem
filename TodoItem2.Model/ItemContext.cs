using Microsoft.EntityFrameworkCore;
using System;
using TodoItem2.Model.Entities;

namespace TodoItem2.Model
{
    public class ItemContext : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public ItemContext(DbContextOptions<ItemContext> options): base(options)
        {
        }
    }
}
