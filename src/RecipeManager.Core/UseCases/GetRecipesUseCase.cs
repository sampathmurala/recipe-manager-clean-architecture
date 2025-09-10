using RecipeManager.Core.Entities;
using RecipeManager.Core.Gateways;

namespace RecipeManager.Core.UseCases
{
    public class GetRecipesUseCase
    {
        private readonly IRecipeGateway _recipeGateway;

        public GetRecipesUseCase(IRecipeGateway recipeGateway)
        {
            _recipeGateway = recipeGateway;
        }

        public async Task<List<Recipe>> ExecuteAsync()
        {
            // The business rule for this use case is simply to retrieve all recipes.
            return await _recipeGateway.GetAllAsync();
        }
    }
}
