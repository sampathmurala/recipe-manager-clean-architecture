using RecipeManager.Core.Entities;
using RecipeManager.Core.Gateways;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager.Core.UseCases
{
    // The use case contains our application-specific business logic.
    // It orchestrates the flow of data and applies business rules.
    // It depends on the IRecipeGateway interface, not a specific implementation.
    public class CreateRecipeUseCase
    {
        private readonly IRecipeGateway _recipeGateway;

        public CreateRecipeUseCase(IRecipeGateway recipeGateway)
        {
            _recipeGateway = recipeGateway;
        }

        public async Task<Recipe> ExecuteAsync(string name, string instructions, List<Ingredient> ingredients)
        {
            // Business Rule: Ensure there is at least one ingredient.
            if (ingredients == null || ingredients.Count == 0)
            {
                throw new ArgumentException("A recipe must have at least one ingredient.", nameof(ingredients));
            }

            var newRecipe = new Recipe(name, instructions, ingredients);
            return await _recipeGateway.SaveAsync(newRecipe);
        }
    }
}
