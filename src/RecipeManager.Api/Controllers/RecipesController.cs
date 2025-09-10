using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using RecipeManager.Api.Models;
using RecipeManager.Core.Entities;
using RecipeManager.Core.UseCases;

namespace RecipeManager.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RecipesController : ControllerBase
    {
        private readonly CreateRecipeUseCase _createRecipeUseCase;
        private readonly GetRecipesUseCase _getRecipesUseCase; // New Use Case

        // The Use Case is injected automatically by ASP.NET Core's DI container.
        public RecipesController(CreateRecipeUseCase createRecipeUseCase, GetRecipesUseCase getRecipesUseCase)
        {
            _createRecipeUseCase = createRecipeUseCase;
            _getRecipesUseCase = getRecipesUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRecipe([FromBody] RecipeRequest request)
        {
            if (request == null || !ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // The controller's job is to translate the DTO to the entity required by the Use Case.
            var ingredients = request.Ingredients.Select(i => new Ingredient(i.Name, i.Quantity, i.Unit)).ToList();

            try
            {
                var newRecipe = await _createRecipeUseCase.ExecuteAsync(request.Name, request.Instructions, ingredients);

                // Return a 201 Created response with the new recipe's ID.
                return CreatedAtAction(nameof(CreateRecipe), new { id = newRecipe.Id }, newRecipe);
            }
            catch (System.ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRecipes()
        {
            // This method would call a use case to get all recipes.
            // For brevity, it's not implemented here.
            var recipes = await _getRecipesUseCase.ExecuteAsync();
            return Ok(recipes);
        }
    }
}