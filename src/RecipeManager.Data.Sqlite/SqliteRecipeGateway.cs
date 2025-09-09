using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using RecipeManager.Core.Entities;
using RecipeManager.Core.Gateways;

namespace RecipeManager.Data.Sqlite.Gateways
{
    // This is our real database gateway. It implements the same interface
    // as our in-memory gateway. This is the key to CLEAN architecture.
    public class SqliteRecipeGateway : IRecipeGateway
    {
        private readonly RecipeDbContext _context;

        public SqliteRecipeGateway()
        {
            _context = new RecipeDbContext();
            // This ensures the database is created and migrations are applied.
            // In a real application, this would be a separate command.
            _context.Database.Migrate();
        }

        public async Task<Recipe> SaveAsync(Recipe recipe)
        {
            await _context.Recipes.AddAsync(recipe);
            await _context.SaveChangesAsync();
            return recipe;
        }

        public async Task<Recipe> GetByIdAsync(Guid recipeId)
        {
            return await _context.Recipes.FindAsync(recipeId);
        }

        public async Task<List<Recipe>> GetAllAsync()
        {
            return await _context.Recipes.ToListAsync();
        }
    }
}
