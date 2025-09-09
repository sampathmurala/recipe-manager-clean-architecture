using Microsoft.EntityFrameworkCore;
using RecipeManager.Core.Entities;
using System.Text.Json;
using System.Collections.Generic;

namespace RecipeManager.Data.Sqlite
{
    // The DbContext is the main class for working with EF Core.
    // It maps our entities to database tables.
    public class RecipeDbContext : DbContext
    {
        // This DbSet represents the "Recipes" table in our database.
        public DbSet<Recipe> Recipes { get; set; }

        public string DbPath { get; }

        public RecipeDbContext()
        {
            // We'll create the database file in the same directory as the executable.
            DbPath = System.IO.Path.Join(System.AppContext.BaseDirectory, "recipes.db");
        }

        // This method configures the database connection.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Data Source={DbPath}");
        }

        // This method configures how our entities map to the database.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure the Recipe entity to store the Ingredients list.
            // SQLite doesn't have a native type for a list of objects,
            // so we serialize it to a JSON string.
            modelBuilder.Entity<Recipe>(entity =>
            {
                entity.HasKey(r => r.Id);
                entity.Property(r => r.Ingredients)
                      .HasConversion(
                          // Convert the List<Ingredient> to a JSON string.
                          v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
                          // Convert the JSON string back to a List<Ingredient>.
                          v => JsonSerializer.Deserialize<List<Ingredient>>(v, (JsonSerializerOptions)null)
                      );
            });
        }
    }
}
