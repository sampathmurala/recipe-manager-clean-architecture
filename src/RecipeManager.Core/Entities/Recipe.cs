using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeManager.Core.Entities
{

    // Our core business object. It represents a recipe.
    // It's a pure C# object with no dependencies on external frameworks.
    public class Recipe
    {
        public Guid Id { get; }
        public string Name { get; private set; }
        public string Instructions { get; private set; }
        public List<Ingredient> Ingredients { get; private set; } = new List<Ingredient>();

        public Recipe(string name, string instructions, List<Ingredient> ingredients)
        {
            Id = Guid.NewGuid();
            Name = name;
            Instructions = instructions;
            Ingredients = ingredients;
        }
    }

}
