using RecipeManager.Core.Entities;
using RecipeManager.Core.UseCases;
using RecipeManager.Data.Sqlite.Gateways;

// We use top-level statements for a concise entry point in modern .NET.
Console.WriteLine("Recipe Manager App - Starting Phase 2");

// --- Dependency Injection ---
// This is where we "wire up" our application. We create a concrete
// instance of the gateway and inject it into our use case.
// The use case doesn't know it's a mock; it only knows about the IRecipeGateway interface.
var recipeGateway = new SqliteRecipeGateway();
var createRecipeUseCase = new CreateRecipeUseCase(recipeGateway);

// --- Application Logic ---
// We define the data for the new recipe.
var ingredients = new List<Ingredient>
{
    new Ingredient("Pasta", 300, "g"),
    new Ingredient("Tomato Sauce", 400, "g"),
    new Ingredient("Garlic", 2, "cloves")
};
var recipeName = "Simple Pasta";
var instructions = "Boil the pasta. Add sauce and garlic.";

// Call the use case to perform the business operation.
try
{
    Console.WriteLine($"\nAttempting to create recipe: '{recipeName}'");
    var newRecipe = await createRecipeUseCase.ExecuteAsync(recipeName, instructions, ingredients);

    // Print the result.
    Console.WriteLine("\nRecipe created successfully!");
    //Console.WriteLine("------------------------------");
    //Console.WriteLine($"ID: {newRecipe.Id}");
    //Console.WriteLine($"Name: {newRecipe.Name}");
    //Console.WriteLine($"Instructions: {newRecipe.Instructions}");
    //Console.WriteLine("Ingredients:");
    //foreach (var ingredient in newRecipe.Ingredients)
    //{
    //    Console.WriteLine($"- {ingredient.Quantity} {ingredient.Unit} of {ingredient.Name}");
    //}


    // Now, let's prove it's in the database by retrieving it.
    Console.WriteLine("\nRetrieving all recipes from the database...");
    var allRecipes = await recipeGateway.GetAllAsync();

    if (allRecipes.Count > 0)
    {
        Console.WriteLine($"\nFound {allRecipes.Count} recipe(s) in the database:");
        foreach (var recipe in allRecipes)
        {
            Console.WriteLine($"-> {recipe.Name} (ID: {recipe.Id})");
        }
    }
    else
    {
        Console.WriteLine("No recipes found in the database.");
    }
}
catch (ArgumentException ex)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine($"\nError creating recipe: {ex.Message}");
    Console.ResetColor();
}

Console.WriteLine("\nApp finished.");
