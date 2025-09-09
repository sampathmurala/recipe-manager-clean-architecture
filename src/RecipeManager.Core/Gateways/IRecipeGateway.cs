using RecipeManager.Core.Entities;

namespace RecipeManager.Core.Gateways
{
    // The gateway interface defines a contract for data access.
    // It is an abstract representation of a data store.
    // Our use cases will depend on this interface, not a concrete implementation.
    public interface IRecipeGateway
    {
        Task<Recipe> SaveAsync(Recipe recipe);
        Task<Recipe> GetByIdAsync(Guid recipeId);
        Task<List<Recipe>> GetAllAsync();
    }
}
