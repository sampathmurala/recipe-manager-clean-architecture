using Xunit;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;

using RecipeManager.Core.Entities;
using RecipeManager.Core.UseCases;
using RecipeManager.Infrastructure.Gateways;

namespace RecipeManager.Tests.Core.UseCases
{
    // This test class verifies the CreateRecipeUseCase.
    // It uses a real-world dependency (the InMemoryRecipeGateway) to demonstrate its functionality.
    public class TestCreateRecipeUseCase
    {
        [Fact]
        public async Task ExecuteAsync_WithValidRecipe_ReturnsRecipeAndSavesIt()
        {
            // Arrange
            var gateway = new InMemoryRecipeGateway();
            var useCase = new CreateRecipeUseCase(gateway);
            var ingredients = new List<Ingredient>
            {
                new Ingredient("Flour", 2.5, "cups"),
                new Ingredient("Sugar", 1, "cup")
            };
            var recipeName = "Basic Cake";
            var instructions = "Mix everything and bake.";

            // Act
            var createdRecipe = await useCase.ExecuteAsync(recipeName, instructions, ingredients);

            // Assert
            Assert.NotNull(createdRecipe);
            Assert.Equal(recipeName, createdRecipe.Name);
            var retrievedRecipe = await gateway.GetByIdAsync(createdRecipe.Id);
            Assert.NotNull(retrievedRecipe);
            Assert.Equal(createdRecipe.Id, retrievedRecipe.Id);
        }

        [Fact]
        public async Task ExecuteAsync_WithNoIngredients_ThrowsArgumentException()
        {
            // Arrange
            var gateway = new InMemoryRecipeGateway();
            var useCase = new CreateRecipeUseCase(gateway);
            var ingredients = new List<Ingredient>(); // Empty list

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentException>(() => useCase.ExecuteAsync("Empty Dish", "Do nothing.", ingredients));
        }
    }
}
