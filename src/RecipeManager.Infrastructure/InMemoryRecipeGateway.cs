using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using RecipeManager.Core.Entities;
using RecipeManager.Core.Gateways;

namespace RecipeManager.Infrastructure.Gateways
{
	// This is a concrete implementation of the IRecipeGateway interface.
	// It serves as a mock data store for testing and development.
	// Our core logic has no knowledge of this class's existence.
	public class InMemoryRecipeGateway : IRecipeGateway
	{
		private readonly Dictionary<Guid, Recipe> _recipes = new Dictionary<Guid, Recipe>();

		public Task<Recipe> SaveAsync(Recipe recipe)
		{
			_recipes[recipe.Id] = recipe;
			return Task.FromResult(recipe);
		}

		public Task<Recipe> GetByIdAsync(Guid recipeId)
		{
			_recipes.TryGetValue(recipeId, out var recipe);
			return Task.FromResult(recipe);
		}

		public Task<List<Recipe>> GetAllAsync()
		{
			return Task.FromResult(_recipes.Values.ToList());
		}
	}
}
